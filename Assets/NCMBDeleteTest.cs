using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class NCMBDeleteTest : MonoBehaviour
{

    private void Start()
    {
        InitNCMBBoard();
    }

    void InitNCMBBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Nightmare");
        query.AddDescendingOrder("Score");
        
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Data Failed" + e.ErrorMessage);
            }
            else
            {
                int cnt = 0;
                foreach (NCMBObject obj in objList)
                {
                    if (cnt == 1) { Debug.Log("DeleteAsync"); obj.DeleteAsync(); }

                    cnt++;
                }

            }
        });
    }
}
