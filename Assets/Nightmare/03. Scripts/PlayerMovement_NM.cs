using UnityEngine;
using System;

public class PlayerMovement_NM : MonoBehaviour
{
    public float speed = 6f;
    public Rigidbody playerRB;
    public Animator anim;

    private Vector3 movement;

    private int floorMask;
    private float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        if (GameManager_NM.singleton.gameMode != 1) return;

        // GetAxisRaw : -1, 0, 1 중 하나 리턴, 즉각적인 움직임에 적합
        // GetAix : -1.0f ~ 1.0f 사이의 값 리턴, 부드러운 움직임에 적합 
        float h = Input.GetAxisRaw("Horizontal"); // 좌:-1, 우:1 
        float v = Input.GetAxisRaw("Vertical"); // 하:-1, 상:1

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // 전달된 값에 따라 vector의 방향 정함
        movement.Set(h, 0f, v);
        // 모든 방향에 대해 크기를 1로 만들고 (nomarlized), 벡터의 정규화(normalized)
        // x축 1, y축 1 이라면 그 사이 대각선은 루트2므로 1보다 커짐, 즉 두키를 동시에 눌렀을때 대각선으로 가는데
        // 이떄만 속도가 더 빨라지므로 정규화 필요. 
        // vector의 크기를 정한다
        movement = movement.normalized * speed * Time.deltaTime;
        // vector의 크기와 방향이 결정되었으니, 다음 프레임에서 위치를 지정한다
        playerRB.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // 마우스 위치에서 출발하는 레이져빔 생성
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 빛을 발사하여 맞은 지점을 지정할 예정
        RaycastHit floorHit;

        // 빔을 발사한다. 빔의 길이는 camRayLength(100f)이고,
        // 바닥이(floormask) 아니라면 무시한다.
        // Physics.Raycast : returns true if the ray intersects with a collider, otherwise false 
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // 빔이 맞은 지점(floorHit)과 현재 player가 있는 위치 사이의 차이를 벡터연산한다.
            Vector3 playerToMove = floorHit.point - transform.position;
            playerToMove.y = 0f; // y축 값의 영향력을 없엔다

            // Player를 회전시켜야할 정도(각도)를 구한다
            Quaternion newpos = Quaternion.LookRotation(playerToMove);
            // Player를 회전시킨다
            playerRB.MoveRotation(newpos);

        }
    }

    void Animating(float h, float v)
    {
        // h = 0이고, v = 0이면 player의 움직임 없는 상태이다
        // 그래서 walking = false가 된다
        bool walking = (h != 0f) || (v != 0f);
        anim.SetBool("IsWalking", walking);
    }

}
