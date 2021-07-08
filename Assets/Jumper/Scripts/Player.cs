using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player PlayerInst;

    private Rigidbody2D _rb2d; // Rigidbody 2D

    public SpriteRenderer _renderer; // Sprite renderer

    public int curColorIndex = 0; // 현재 Player의 색상
    private int MaxColorIndex = 3; // black, red, blue

    public float MovePower = 1f;
    private float moveInput;

    // SCORE
    [SerializeField]
    private Text ScoreText; // attach Score Text
    public float topScore = 0.0f;

    // Animation
    public Animator _ani;

    // Sound
    public AudioSource jump_audio;
   
    private void Start()
    {
        //_audio = GetComponent<AudioSource>();
        _rb2d = gameObject.GetComponent<Rigidbody2D>(); // get rigidbody attached to this gameobject
        curColorIndex = 0; // black

        // 게임 시작시 Player의 Rigidbody 무력화
        _rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        // GameEnds가 false인 상태, 즉 게임이 진행중에만 이동가능
        if(!JumperManagerGame.singleton.GameEnds)
        {
            // 이동
            moveInput = Input.GetAxis("Horizontal");
            _rb2d.velocity = new Vector2(moveInput * MovePower, _rb2d.velocity.y);
        }

        
    }


    private void Update()
    {
        // 캐릭터가 이동하는 방향을 바라보게함
        if(moveInput < 0) // move left
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        }
        else if(moveInput > 0) // move right
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
        }

        /*  Jump Animation이 필요할까?
        if(_rb2d.velocity.y > 0) // Jump Animation
        {
            _ani.SetBool("SetJump", true);
        }
        else
        {
            _ani.SetBool("SetJump", false);
        }*/

        // Attack Animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ani.SetTrigger("TriggerAttack");
        }

        // Score: 위로 향할때 && 현재 위치가 topScore보다 높을때 갱신
        if (_rb2d.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }
        ScoreText.text = Mathf.Round(topScore).ToString();
    }

    // 공이 다른 물체와 접촉하면 튕김.
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Player가 아래로 낙하중일때만
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            jump_audio.Play(); // Jump Sound 재생
            // 밟은 발판이 검정발판이 아니고
            if (collision.gameObject.GetComponent<Bounce>() != null && collision.gameObject.GetComponent<Bounce>().MyColIdx != 0)
            {
                // 현재 Player의 색과 다르면 GameOver
                if (collision.gameObject.GetComponent<Bounce>().MyColIdx != curColorIndex
                    && curColorIndex != 0)
                {
                    JumperManagerGame.singleton.setGameOver(); // Game Over         
                    _rb2d.velocity = new Vector2(0, 0); // GameOver시 캐릭 완전히 멈춤

                }
            }

            CurColor(); // 접촉시마다 색 변경
        }
        
    }

    public void CurColor()
    {
        // curColorIndex 변경 : 0, 1, 2, 0, 1, 2 ...
        curColorIndex = (curColorIndex + 1) % MaxColorIndex;
        //Debug.Log(curColorIndex);
        switch (curColorIndex) // curColIndex 값에 따라 색상 변경
        {
            case 0:
                _renderer.color = Color.white;
                break;

            case 1:
                _renderer.color = Color.red;
                break;

            case 2:
                _renderer.color = Color.blue;
                break;
        }
        
    }


    // Called after Intro Ends
    public void IntroEnds()
    {
        // 게임 시작하면서 Player의 Rigidbody 활성 
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
        // Fireball 소환 시작.
        GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().enabled = true;
    }

   

}
