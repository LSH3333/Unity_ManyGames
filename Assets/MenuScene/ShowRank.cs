using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NCMB;
using UnityEngine;

public class ShowRank : MonoBehaviour
{
    // NCMB 데이터베이스에서 정보가져와 랭크창 표시.
    // _txtRank나 _NCBMRank 둘 중 하나만 사용해야함 
    public Text _NCMBRank;
    public string gamename;

    public void ClickBtn(string _gameName)
    {
        gamename = _gameName;
        InitNCMBBoard();
        InstaniateRankingPanel();
    }

    private void InstaniateRankingPanel()
    {
        GameObject resource = (GameObject)Resources.Load("ShowRanking");
        GameObject obj = Instantiate(resource, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        _NCMBRank = obj.transform.GetChild(1).GetComponent<Text>();
        
        ((RectTransform)obj.transform).anchoredPosition = new Vector2(0, 0);
        
    }

    // NCMB Database에서 랭킹정보 가져와서 10위까지 띄움  
    void InitNCMBBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(gamename);
        query.AddDescendingOrder("Score");

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Data Failed" + e.ErrorMessage);
            }
            else
            {
                string res = "";
                _NCMBRank.text = "";
                int rank = 0;
                int cnt = 0; // 10위 까지만 보여줌 
                foreach (NCMBObject obj in objList)
                {
                    res = string.Format("{0:D2}. ", (++rank));
                    res += "                        ";
                    res += obj["Name"] + "          ";
                    res += obj["Score"];

                    _NCMBRank.text += res + "\n";

                    cnt++;
                    if (cnt >= 10) break;
                }

            }
        });
    }

    

}
