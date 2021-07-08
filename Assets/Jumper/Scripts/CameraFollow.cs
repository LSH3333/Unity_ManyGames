using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player에 카메라가 따라가도록 함.
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow; // 따라갈 target. (Player)
    private float HighestYpos = 0; // Player가 도달한 가장 높은 지점 y pos

    private bool flag = true; // update문에서 한번만 실행시키기 위한 용도
    
    private void Update()
    {
        if (JumperManagerGame.singleton.GameEnds) return;

        // 가장 높은 지점 갱신
        if(targetToFollow.position.y >= HighestYpos)
            HighestYpos = targetToFollow.position.y;

        // 화면의 좌,우로 빠져나가면 그 반대로 돌아옴
        if (targetToFollow.position.x < -10) targetToFollow.position = new Vector2(10f, HighestYpos);
        if (targetToFollow.position.x > 10) targetToFollow.position = new Vector2(-10f, HighestYpos);

        // 카메라가 Player를 따라감, 하지만 Player가 다시 아래로 이동했을때는 따라가지 않음.
        transform.position = new Vector3(
            transform.position.x, // 나의 x pos
            HighestYpos, // Player의 y pos
            transform.position.z);

        // 낙하시 게임오버, flag true일때만 (한번만)
        if(targetToFollow.position.y < transform.position.y - 10.2f && flag)
        {
            JumperManagerGame.singleton.setGameOver(); // 낙하 시 게임오버  
            flag = false;
        }

        
    }
}
