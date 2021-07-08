﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Collections.Generic;

public class Angry_Result : MonoBehaviour
{
    public Text _txtRank; // 1~10위 까지의 랭크정보 문자열 출력

    // NCMB 데이터베이스에서 정보가져와 랭크창 표시.
    // _txtRank나 _NCBMRank 둘 중 하나만 사용해야함 
    public Text _NCMBRank; 

    
    
    private void Start()
    {
        SetResult();
        ManageApp.singleton.Save(); // 랭크정보를 저장한다

        // NCMB Database
        SendPlayerDataToNCMB();
        InitNCMBBoard();

        //_txtRank.text = ManageApp.singleton.getRankString();  // 랭크정보를 string 형태로 가져옴
    }

    void SetResult()
    {
        // best score update
        ManageApp.singleton.updateBestScore(Angry_ManagerGame.singleton.Score);
        //ManageApp.singleton.updateBestScore(3);

        var list_name = new ArrayList();
        var list_score = new ArrayList();
        string out_name;
        int out_score;
        for(int i = 0; i < 10; i++) // 기존 데이터 모두 리스트로 가져옴
        {
            ManageApp.singleton.GetData(i, out out_name, out out_score);
            list_name.Add(out_name);
            list_score.Add(out_score);
        }

        for(int i = 0; i < 10; i++)
        {
            // 항목을 순차적으로 탐색하면서 현재의 스코어를 삽입할 위치를 찾는다
            if((int)Angry_ManagerGame.singleton.Score > (int) list_score[i])
            {
                list_name.Insert(i, ManageApp.singleton.NickName);
                list_score.Insert(i, (int)Angry_ManagerGame.singleton.Score);
                break; // 랭크 데이터로 삽입했기 때문에 탐색 종료
            }
        }

        // 새롭게 구성된 랭크 데이터 저장
        for(int i = 0; i < 10; i++)
        {
            ManageApp.singleton.SetData(i, (string)list_name[i], (int)list_score[i]);
        }
    }

    public void SendPlayerDataToNCMB()
    {
        // 해당하는 게임이름의 클래스 NCMB 오브젝트 생성 
        NCMBObject obj = new NCMBObject(ManageApp.singleton.gameName);

        // 이름과 점수 데이터베이스에 저장 
        obj.Add("Name", ManageApp.singleton.NickName);
        obj.Add("Score", Angry_ManagerGame.singleton.Score);

        obj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
                Debug.Log("NCMB Save Failed");
            else
                Debug.Log("NCMB Save Successed");
        });
    }

    void InitNCMBBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ManageApp.singleton.gameName);
        query.AddDescendingOrder("Score");

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Data Failed");
            }
            else
            {
                string res = "";
                _NCMBRank.text = "";
                int rank = 0;

                foreach (NCMBObject obj in objList)
                {
                    res = string.Format("{0:D2}. ", (++rank));
                    res += obj["Name"] + ", ";
                    res += obj["Score"];

                    _NCMBRank.text += res + "\n";
                }

            }
        });
    }
}
