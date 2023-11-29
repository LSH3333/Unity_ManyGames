using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NCMB;
using UnityEngine;
using UnityEngine.Networking;
using Management;

/*
    
    각 게임들의 씬에서 Manager 스크립트를 갖고있는 게임오브젝트는 "GameManager" 태그를 달아야한다. 
    manager에 현재 게임의 Manager를 찾을때 태그로 게임오브젝트를 찾기 때문이다.

    * 새로운 게임이 추가되면:
    1. 추가된 게임씬에서 Manager 스크립트를 갖고있는 게임오브젝트의 태그를 "GameManager"로 설정한다.
    2. boardResult 게임오브젝트의 Result.cs에 Text _NCMBRank 변수를 boardResult의 자식인 txtRank에 연결한다

    *각 게임의 매니저는 게임 종료시 결과창을 띄우는 SetGameOver() 함수를 포함해야한다. 
*/

public class Result : Manage
{
    // NCMB 데이터베이스에서 정보가져와 랭크창 표시.
    // _txtRank나 _NCBMRank 둘 중 하나만 사용해야함 
    public Text _NCMBRank;

    // BestScore 표시할 텍스트 창 
    //public Text txtBest;

    private Button replayButton, mainButton;
    // 게임에 대한 정보(점수 등)은 GameManager클래스의 부모인 Manage에 담겨있음 
    private GameObject gameManagerObj;

    // For InitNCMBBoard()
    private Text noTxt, usernameTxt, scoreTxt;

    private new void Awake()
    {
        gameManagerObj = GameObject.Find("GameManager");
        SetButton();
    }


    private void Start()
    {
        // 현재 게임, 현재 유저, 점수 정보를 NCMB Database로 보내고
        // 방금 보내진 유저 정보까지 포함된 랭킹 10위까지의 정보를 가져옴. 

        StartCoroutine(getRank());        
    }

    // InitNCMBBoard를 위한 변수들의 레퍼런스 설정 
    private void InitResultBoard()
    {
        GameObject NCMBRankObj = transform.GetChild(1).gameObject;
        noTxt = NCMBRankObj.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        usernameTxt = NCMBRankObj.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        scoreTxt = NCMBRankObj.transform.GetChild(2).GetChild(0).GetComponent<Text>();
    }

    // 현재 SendPlayerDataToNCMB()에서 정보 저장과, InitNCMBBoard()에서 정보 가져오는것이
    // 병렬적으로 일어나서 현재 플레이어 정보가 다 저장이 안된상태에서 랭킹 데이터 가져와
    // 현재 플레이어 정보가 랭킹창에 포함되지 않는 경우가 있음. 

    // 따라서 데이터베이스로 데이터를 보내고 1초 대기후 데이터베이스에서 랭킹 정보 가져오도록함. 
    IEnumerator getRank()
    {
        // NCMB 
        if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
        {
            Debug.Log("ManageApp.singleton.DBtype == ManageApp.DB.NCMB");
            SendPlayerDataToNCMB();
            yield return new WaitForSeconds(1f);
            InitNCMBBoard();
        }
        // PostgreSQL 
        else
        {
            Debug.Log("ManageApp.singleton.DBtype == ManageApp.DB.Postgre");
            SendPlayerDataToPostgreSQL();
            yield return new WaitForSeconds(1f);
            InitPostgreSQLBoard();
        }
                
        InitResultBoard();
        
    }

    // 현재 게임의, 현재 유저 정보를 NCMB Database로 보냄 
    public void SendPlayerDataToNCMB()
    {
        // 해당하는 게임이름의 클래스 NCMB 오브젝트 생성 
        NCMBObject obj = new NCMBObject(ManageApp.singleton.gameName);

        // 이름과 점수 데이터베이스에 저장 
        obj.Add("Name", ManageApp.singleton.NickName);
        obj.Add("Score", gameManagerObj.GetComponent<Manage>().score);

        obj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
                Debug.Log("NCMB Save Failed");
            else
                Debug.Log("NCMB Save Successed");
        });
    }

    // 점수순으로 유저 데이터 불러옴 
    private void InitNCMBBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ManageApp.singleton.gameName);
        // 점수 내림차순 정렬  
        query.AddDescendingOrder("Score");

        // Board Init 전에 초기화 
        noTxt.text = "";
        usernameTxt.text = "";
        scoreTxt.text = "";

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Score Data Failed" + e.ErrorMessage);
            }
            else
            {
                string res = "";
                int rank = 0;
                int cnt = 0; // 10위 까지만 보여줌

                foreach (NCMBObject obj in objList)
                {
                    noTxt.text += string.Format("{0:D2}. ", (++rank)) + "\n";
                    usernameTxt.text += obj["Name"] + "\n";
                    scoreTxt.text += obj["Score"] + "\n";

                    cnt++;
                    if (cnt >= 10) break;
                }

            }
        });
    }

    //////////// HTTP
    // db 로 결과 데이터 전송 
    public void SendPlayerDataToPostgreSQL()
    {
        StartCoroutine(SendPlayerDataToPostgreSQLIE());
    }

    IEnumerator SendPlayerDataToPostgreSQLIE()
    {
        string url = ManageApp.singleton.Url + "rank/uploadRank";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name", ManageApp.singleton.NickName));
        formData.Add(new MultipartFormDataSection("score", gameManagerObj.GetComponent<Manage>().score.ToString()));        
        formData.Add(new MultipartFormDataSection("gameName", ManageApp.singleton.gameName));

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, formData);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete");
            string response = unityWebRequest.downloadHandler.text;
            Debug.Log("response = " + response);
        }
    }
    // db 에서 10등까지 랭킹 가져옴 
    private void InitPostgreSQLBoard()
    {
        StartCoroutine(InitPostgreSQLBoardIE());
    }

    IEnumerator InitPostgreSQLBoardIE() {
        Debug.Log("InitPostgreSQLBoardIE()");
        string url = ManageApp.singleton.Url + "rank/getRank";
        url += "?gameName=" + ManageApp.singleton.gameName;

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else // 성공 
        {
            Debug.Log("Http InitPostgreSQLBoard Success");
            string jsonString = unityWebRequest.downloadHandler.text;
            RankListResponse rankListResponse = JsonUtility.FromJson<RankListResponse>("{\"rankList\":" + jsonString + "}");

            int rank = 0;
            foreach (RankDto rankDto in rankListResponse.rankList)
            {
                noTxt.text += string.Format("{0:D2}. ", (++rank)) + "\n";
                usernameTxt.text += rankDto.name + "\n";
                scoreTxt.text += rankDto.score + "\n";
            }

            
        }
    }

    [System.Serializable]
    public class RankDto
    {
        public string name;
        public int score;
        public string gameName;
    }

    public class RankListResponse
    {
        public List<RankDto> rankList;
    }



    //// NCMB Database에서 랭킹정보 가져와서 10위까지 띄움  
    //void InitNCMBBoard()
    //{
    //    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ManageApp.singleton.gameName);
    //    query.AddDescendingOrder("Score");

    //    query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
    //    {
    //        if (e != null)
    //        {
    //            Debug.Log("NCMB Get Data Failed" + e.ErrorMessage);
    //        }
    //        else
    //        {
    //            string res = "";
    //            _NCMBRank.text = "";
    //            int rank = 0;
    //            int cnt = 0; // 10위 까지만 보여줌 
    //            foreach (NCMBObject obj in objList)
    //            {                    
    //                res = string.Format("{0:D2}. ", (++rank));
    //                res += "          ";
    //                res += obj["Name"] + ", ";
    //                res += obj["Score"];

    //                _NCMBRank.text += res + "\n";                    
    //                cnt++;
    //                if (cnt >= 10) break;
    //            }

    //        }
    //    });        
    //}

    // ResultBoard의 child인 Button에 OnClick 이벤트 할당 
    private void SetButton()
    {
        replayButton = gameObject.transform.GetChild(2).GetComponent<Button>();
        mainButton = gameObject.transform.GetChild(3).GetComponent<Button>();

        // ManageApp에서 게임이름 가져옴 
        string replaySceneName = ManageApp.singleton.gameName + "_Game";
        string mainSceneName = ManageApp.singleton.gameName + "_Title";

        // 람다식 
        // 현재 클래스도 Manage 클래스를 상속받지만, 지금 메모리에 올라가있는 fade 오브젝트는 
        // Manage를 상속받는 GameManager 클래스가 Awake()로 소환한것이므로 GameManager의 레퍼런스를 불러와야함 
        GameObject _fadeobj = GameObject.Find("GameManager");
        replayButton.onClick.AddListener(() => { _fadeobj.GetComponent<Manage>().SetFadeout(replaySceneName); });
        mainButton.onClick.AddListener(() => { _fadeobj.GetComponent<Manage>().SetFadeout(mainSceneName); });
       
    }


    public override void SetStart()
    {        
    }

}
