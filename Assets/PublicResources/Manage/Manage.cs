using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

namespace Management
{
    // 부모 클래스 
    public abstract class Manage : MonoBehaviour
    {
        private GameObject _fadeobj; // Fade 프리팹 인스턴스 개체 참조할 변수 
        private int _fadeSiblingIndex; // Fade 인스턴스를 최상위 위치로 유지할 목적 

        [HideInInspector]
        public int score;

        // BestScore은 부모에서 관리
        [HideInInspector]
        public Text _txtBest;

        

        // 자식 클래스 override 사용할 목적 
        protected virtual void Awake()
        {
            // 시작하면서 Fade 프리팹 소환 
            _fadeobj = InstantiateUI("Fade", "Canvas", true);
            _fadeSiblingIndex = _fadeobj.transform.GetSiblingIndex();
        }

        // pn: prefab name, cn : canvas name
        // isfull ? canvas 가득 채우도록 배치 : 캔버스 중앙으로 배치 
        public GameObject InstantiateUI(string pn, string cn, bool isfull)
        {
            GameObject resource = (GameObject)Resources.Load(pn);
            GameObject obj = Instantiate(resource, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(cn).transform);

            if (isfull)
            {
                ((RectTransform)obj.transform).offsetMax = new Vector2(0, 0);
            }                
            else
            {
                ((RectTransform)obj.transform).anchoredPosition = new Vector2(0, 0);                
            }
            ((RectTransform)obj.transform).localScale = new Vector2(1f, 1f);

            if (!pn.Equals("Fade")) obj.transform.SetSiblingIndex(_fadeSiblingIndex);

            return obj;
        }

        // fade out 작동시키고, 다음 씬으로 넘어가도록함 
        public void SetFadeout(string nextScene)
        {            
            _fadeobj.GetComponent<Fade>().setNextScene(nextScene);
            _fadeobj.SetActive(true);
            _fadeobj.GetComponent<Fade>().setFadeOut();
        }

        // 각각의 게임마다 게임시 시작될때 처리할 내용이 다르기 때문에 클래스에서 재정의 (override) 해서
        // 사용하도록 강제시킴 
        public abstract void SetStart();


        // 현재 게임의 "Score"를 정렬해서 가져와서 가장 높은 점수인 BestScore를 찾는다 
        public void GetBestScore(string gameName)
        {
            if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
            {
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(gameName);
                query.AddDescendingOrder("Score");

                query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
                {
                    if (e != null)
                    {
                        Debug.Log("NCMB get BestScore Failed");
                    }
                    else
                    {
                        _txtBest.text = "BestScore: ";
                        int cnt = 0;
                        // "Score"를 정렬해서 가져왔으므로 첫 원소가 가장 높은 점수 즉 BestScore이다.    
                        foreach (NCMBObject obj in objList)
                        {
                            _txtBest.text += obj["Score"].ToString();
                            cnt++;

                            if (cnt >= 1) break;
                        }
                    }
                });
            }
            else
            {
                HttpGetBestScore(gameName);
            }
            
        }



        // db 에서 10등까지 랭킹 가져옴 
        private void HttpGetBestScore(string gameName)
        {
            StartCoroutine(HttpGetBestScoreIE(gameName));
        }

        IEnumerator HttpGetBestScoreIE(string gameName)
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

                _txtBest.text = "BestScore: " + rankListResponse.rankList[0].score;                
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
}


