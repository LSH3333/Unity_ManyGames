using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Collections.Generic;
using MyEngine;

namespace Management
{
    // 부모 클래스 
    public abstract class Manage : MyEngine.Manage
    {

        // 현재 게임의 "Score"를 정렬해서 가져와서 가장 높은 점수인 BestScore를 찾는다 
        public void GetBestScore(string gameName)
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
                        _txtBest.text += obj["Score"];
                        cnt++;

                        if (cnt >= 1) break;
                    }
                }
            });
        }

    }
}


