using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NCMB;
using UnityEngine;
using UnityEngine.Networking;

public class ShowRank : MonoBehaviour
{
    public string gamename;

    private Text noTxt, usernameTxt, scoreTxt;

    

    public void ClickBtn(string _gameName)
    {
        gamename = _gameName;
        InstaniateRankingPanel();
        if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
        {
            InitNCMBBoard();
        }
        else
        {
            InitPostgreSQLBoard(gamename);
        }
    }

    private void InstaniateRankingPanel()
    {
        GameObject resource = (GameObject)Resources.Load("ShowRanking");
        GameObject obj = Instantiate(resource, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        ((RectTransform)obj.transform).anchoredPosition = new Vector2(0, 0);

        GameObject NCMBRankObj = obj.transform.GetChild(1).gameObject;                
        noTxt = NCMBRankObj.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        usernameTxt = NCMBRankObj.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        scoreTxt = NCMBRankObj.transform.GetChild(2).GetChild(0).GetComponent <Text>();
    }


    
    // 점수순으로 유저 데이터 불러옴 
    private void InitNCMBBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(gamename);
        // 계정 생성 순 정렬 
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
                //privateDataTxt.text = "";
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


    // db 에서 10등까지 랭킹 가져옴 
    private void InitPostgreSQLBoard(string gameName)
    {
        StartCoroutine(InitPostgreSQLBoardIE(gameName));
    }

    IEnumerator InitPostgreSQLBoardIE(string gameName)
    {
        Debug.Log("InitPostgreSQLBoardIE()");
        string url = ManageApp.singleton.Url + "rank/getRank";
        url += "?gameName=" + gameName;

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
}
