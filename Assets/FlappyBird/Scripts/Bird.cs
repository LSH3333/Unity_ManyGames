using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    public float upForce = 200f;

    private Rigidbody2D _rb2d;
    private Animator _anim;

    private PolygonCollider2D _coll; // isTrigger 값 조정
    private SpriteRenderer _renderer; // Bird의 껌뻑껌뻑 효과 처리

    // true while bird blinking
    public bool isBlink = false;

    private bool isShow = true;
    private int blinkCount = 5;
    private float fBlink = 3f;


    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>(); // Attached된 Animator 컴포넌트
        _rb2d.bodyType = RigidbodyType2D.Kinematic; // Rigidbody 무력화 (중력 x)

        _renderer = GetComponent<SpriteRenderer>();
        _coll = GetComponent<PolygonCollider2D>();
        _coll.isTrigger = true; // 처음에는 충돌시 물리적 처리를 하지 않는다.
    }

    private void Update()
    {
        //Debug.Log(isBlink);
        if (FlappyBird_ManagerGame.inst.gameMode != 1) return; // Game Play가 아니면 터치 대응 x

        if (Input.GetMouseButtonDown(0))
        {
            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce(new Vector2(0f, upForce));
            _anim.SetTrigger("SetFlap"); // 클릭시 SetFlap
        }

        BirdBlink(); // 매 프레임마다 껌뻑껌뻑 랜더링 상태를 확인 및 처리.
    }

    // Bird가 다른 Obstacle과 충돌했을때, 이때는 이미 Bird의 life는 모두 소진된 상태임
    private void OnCollisionEnter2D(Collision2D other)
    {
        // 마지막 충돌시 gameMode=2(game Result)로 바뀌고 이후에는 충돌 무시됨 
        if (FlappyBird_ManagerGame.inst.gameMode == 2) return;
        _anim.SetTrigger("SetDie");
        FlappyBird_ManagerGame.inst.SetGameOver();
    }

    private void OnTriggerEnter2D(Collider2D col) // Collider가 겹처칠때 호출
    {
        // 바닥과 충돌시 바로 게임오버 처리 
        if(col.gameObject.tag == "HorzScroll")
        {
            FlappyBird_ManagerGame.inst.SetGameOver();
            return;
        }
        if (isBlink) return; // 껌뻑거리는 상태면 그냥 종료
        if (col.gameObject.tag != "Column") return; // 충돌 오브젝트가 Column인지 확인

        isBlink = true;  // 껌뻑거리기 시작
        SendBlinkToManager(true); // 깜빡인다고 GameManager에 신호를 보냄.
        fBlink = 3f; // 3초 동안

        FlappyBird_ManagerGame.inst.SetLifeDown();
    }

    void BirdBlink()
    {
        if (!isBlink) return; // 껌뻑거리는 상황아니면 종료

        if (--blinkCount <= 0)
        {
            blinkCount = 5;
            if (isShow = !isShow) _renderer.color = Color.white; // 불투명. 보인다
            else _renderer.color = Color.clear; // 투명. 안보임
        }

        fBlink -= Time.deltaTime;
        if (fBlink < 0f) // 껌뻑거리는 시간 종료
        {
            isBlink = false; // 껌뻑거리는 상태 제거
            SendBlinkToManager(false); // 깜빡임이 해제되었다고 GameManager에 신호를 보냄
            _renderer.color = Color.white; // 원래대로 불투명으로 복귀

            // HP 모두 소진시, isTrigger을 false로 함으로서 충돌가능하게함
            if (FlappyBird_ManagerGame.inst.Life == 0) _coll.isTrigger = false;
        }
    }

    // to prevent scoring when blinking. send if bird is blinking to GameManager. 
    public void SendBlinkToManager(bool state)
    {
        if (state)
            GameObject.Find("GameManager").GetComponent<FlappyBird_ManagerGame>().CheckBirdBlinking = true;
        else
            GameObject.Find("GameManager").GetComponent<FlappyBird_ManagerGame>().CheckBirdBlinking = false;
    }
}
