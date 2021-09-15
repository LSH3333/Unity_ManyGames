using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_IntroControl : MonoBehaviour
{
    public GameObject bird;
    public ObstaclePool obstaclePool;

    public void IntroEnds()
    {
        // Game Play Mode로 변경
        FlappyBird_ManagerGame.inst.gameMode = 1; 
        // Bird 움직임 시작 
        bird.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // 장애물 생성 시작 
        obstaclePool.InitColumnCreate();

        // tag가 HorzScroll인 모든 게임오브젝트 찾음
        GameObject[] objs = GameObject.FindGameObjectsWithTag("HorzScroll");
        foreach (var o in objs) // 횡스크롤 시작
            o.SendMessage("GameStart");
    }
}
