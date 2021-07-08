using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NCMB;
using UnityEngine;

/*
    기존과 다르게
    ManagerParent.cs 라는 부모 클래스를 선언.
    각각의 게임의 Manager들은 (Angry_Manager 등) ManagerParent를 상속받음.
    
    실행될때 Awake()에서 ManagerParent manager에 현재 게임의 Manager가 담긴다.
    
    각 게임들의 씬에서 Manager 스크립트를 갖고있는 게임오브젝트는 "GameManager" 태그를 달아야한다. 
    manager에 현재 게임의 Manager를 찾을때 태그로 게임오브젝트를 찾기 때문이다.

    * 새로운 게임이 추가되면:
    1. 추가된 게임씬에서 Manager 스크립트를 갖고있는 게임오브젝트의 태그를 "GameManager"로 설정한다.
    2. boardResult 게임오브젝트의 Result.cs에 Text _NCMBRank 변수를 txtRank에 연결한다 
*/

public class Result : MonoBehaviour
{
    // NCMB 데이터베이스에서 정보가져와 랭크창 표시.
    // _txtRank나 _NCBMRank 둘 중 하나만 사용해야함 
    public Text _NCMBRank;

    // 각 게임들의 Manager들이 담긴다 
    ManagerParent manager;

    private void Awake()
    {
        // ManagerParent의 자식 중 하나인 클래스에 접근 
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerParent>();
    }

    private void Start()
    {
        SetResult();
        ManageApp.singleton.Save();

        // NCMB Database
        SendPlayerDataToNCMB();
        InitNCMBBoard();

    }

    private void SetResult()
    {
        
        ManageApp.singleton.updateBestScore(manager.score);

        var list_name = new ArrayList();
        var list_score = new ArrayList();
        string out_name;
        int out_score;
        for (int i = 0; i < 10; i++) // 기존 데이터 모두 리스트로 가져옴
        {
            ManageApp.singleton.GetData(i, out out_name, out out_score);
            list_name.Add(out_name);
            list_score.Add(out_score);
        }

        for (int i = 0; i < 10; i++)
        {
            // 항목을 순차적으로 탐색하면서 현재의 스코어를 삽입할 위치를 찾는다
            if (manager.score > (int)list_score[i])
            {
                list_name.Insert(i, ManageApp.singleton.NickName);
                list_score.Insert(i, manager.score);
                break; // 랭크 데이터로 삽입했기 때문에 탐색 종료
            }
        }

        // 새롭게 구성된 랭크 데이터 저장
        for (int i = 0; i < 10; i++)
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
        obj.Add("Score", manager.score);

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
