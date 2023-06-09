using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using NCMB;


public class ManagerParent_removed : MonoBehaviour
{
    [HideInInspector]
    public int score;

    // BestScore은 부모에서 관리
    [HideInInspector]
    public Text _txtBest;


    // 현재 게임의 "Score"를 정렬해서 가져와서 가장 높은 점수인 BestScore를 찾는다 
    public void GetBestScore()
    {        

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ManageApp.singleton.gameName);
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
                    _txtBest.text += obj["Score"];
                    cnt++;
                    
                    if (cnt >= 1) break;
                }
            }
        });
                
    }
}
