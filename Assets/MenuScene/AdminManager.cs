using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;
using System.Collections.Generic;

public class AdminManager : MonoBehaviour
{
    public Text privateDataTxt;

    public void Awake()
    {
        InitAdminBoard();
    }

    // get user private data  
    private void InitAdminBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserPrivateData");
        query.AddDescendingOrder("AccoutCreateDate");

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Private Data Failed" + e.ErrorMessage);
            }
            else
            {
                string res = "";
                privateDataTxt.text = "";
                int rank = 0;
                int cnt = 0; // 10위 까지만 보여줌 
                foreach (NCMBObject obj in objList)
                {
                    res = string.Format("{0:D2}. ", (++rank));
                    res += " ";
                    res += obj["UserName"] + "          ";
                    res += obj["UserPw"] + "          ";
                    res += obj["AccountCreateDate"];

                    privateDataTxt.text += res + "\n";

                    cnt++;
                    if (cnt >= 10) break;
                }

            }
        });
    }
}
