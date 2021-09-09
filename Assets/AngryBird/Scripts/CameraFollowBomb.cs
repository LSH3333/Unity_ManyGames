﻿using UnityEngine;

public class CameraFollowBomb : MonoBehaviour {

    public int num_target = 3; // target Bomb의 갯수
    //public GameObject[] targetToFollow;
    public GameObject[] targetToFollow; // Camera가 따라갈 target
    
    private AB_CreateMap createMap;
    public GameObject[] bombLeft;
    public int bombLeftIdx = 0;

    public int CurTargetIdx = 0; // Current Bomb Index

    void Start()
    {
        createMap = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AB_CreateMap>();
    }

    void Update()
    {   /*
        // Bomb의 Spring이 Destroy됐다 = 공이 날라가기 시작함
        if(GameObject.Find("Bomb").GetComponent<Bomb>().SpringDestroy)
        {
            // 카메라가 target을 따라감.
            transform.position = new Vector3(
                targetToFollow.transform.position.x,
                targetToFollow.transform.position.y,
                transform.position.z);
        }
        */
        // spring이 파괴되면
        //if (targetToFollow[CurTargetIdx] == null) return;
        if (CurTargetIdx >= 3)
        {
            GameObject newBomb = GameObject.Find("Bomb(Clone)");
            
            if (newBomb.GetComponent<Bomb>().SpringDestroy)
            {
                transform.position = new Vector3(
                newBomb.transform.position.x,
                newBomb.transform.position.y,
                transform.position.z);
            }
        }
        else if (targetToFollow[CurTargetIdx].GetComponent<Bomb>().SpringDestroy)
        {
            transform.position = new Vector3(
                targetToFollow[CurTargetIdx].transform.position.x,
                targetToFollow[CurTargetIdx].transform.position.y,
                transform.position.z);
        }

        
    }

    // 현재 Bomb의로 카메라 일시적으로 이동
    public void SwitchBomb()
    {
        Debug.Log("CHECK: " + createMap.enemiesDead);
        // 잡은 마리당 10점의 점수 추가 
        Angry_ManagerGame.singleton.score += createMap.enemiesDead * 10;
        // 현재점수판 업데이트
        Angry_ManagerGame.singleton.UpdateCurScore();

        // bombLeftIdx >= 3이라면 3개의 목숨을 다 썼다는 의미 
        if (bombLeftIdx >= 3 && createMap.enemiesDead != 3)
        {
            GameObject.Find("GameManager").GetComponent<GameOverFunction>().setGameOver();
        }

        // 3마리 다 잡지 못했다면 목숨 감소
        if(createMap.enemiesDead != 3 && bombLeftIdx < 3)
        {
            bombLeft[bombLeftIdx++].SetActive(false);
        }

        //CurTargetIdx = (++CurTargetIdx) % 3;
        CurTargetIdx = ++CurTargetIdx;
        //Debug.Log("CurTargetidx: " + CurTargetIdx);
        // 카메라 초기 위치로 이동
        transform.position = new Vector3(
            0f, 0f, transform.position.z);

        
    }
}
