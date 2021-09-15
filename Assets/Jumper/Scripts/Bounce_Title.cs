using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class Bounce_Title : MonoBehaviour
{

    private float jumpPower = 0f;

    private float JumpPowerRate = 500f;

    public SpriteRenderer _renderer;

    private int MaxColorIndex = 3;
    public int MyColIdx = 3;
    //  Black: 0, Red: 1, Blue: 2, 

    private Collider2D _col;
    private Rigidbody2D _rb2d;
    private int dir = 1; // Moving direction
    public int MovingPercentage;

    private void Awake()
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;
        if (currentSceneName == "MenuScene") gameObject.GetComponent<Bounce>().enabled = false;


        _renderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        _col = gameObject.GetComponent<Collider2D>();
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpPower = JumpPowerRate;
        myColIndex();

        MovingPercentage = Random.Range(0, 10);

    }

    private void FixedUpdate()
    {
        // 첫 발판은 움직이지 않도록
        if (gameObject.name == "Platform_primary") return;
        
        // 확률로 인해 어떤 발판들은 움직이도록
        if (MovingPercentage == 1)
        {
            MovingPlatforms();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Jumper_Title") return;

        // Game종료면 발판의 Collider2D 비활성.
        if (JumperManagerGame.singleton.GameEnds)
        {
            _col.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "Weapon-Sword") { Debug.Log("weapon"); return; }
        //Debug.Log("weapon");
        // Player의 색과 밟은 발판의 색이 같으면 더 높이 점프 단 기본색은 예외
        if (collision.gameObject.GetComponent<Player>().curColorIndex == MyColIdx
            && collision.gameObject.GetComponent<Player>().curColorIndex != 0)
            jumpPower = JumpPowerRate;
        else
            jumpPower = JumpPowerRate;

        // Player가 아래로 낙하중일때만
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpPower));
        }

    }

    // 이 발판의 색이 무엇인가. MyColIdx에 저장.
    private void myColIndex()
    {
        if (gameObject.name == "platform_Red(Clone)")
        { MyColIdx = 1; }// Red
        else if (gameObject.name == "platform_Blue(Clone)")
        { MyColIdx = 2; }// Blue
        else
        {
            MyColIdx = 0; // black   
        }
    }

    // 움직이는 발판들
    private void MovingPlatforms()
    {
        // 화면 양쪽 끝 도달시 방향 반대
        if (transform.position.x >= 10f || transform.position.x <= -10f) dir *= -1;
        _rb2d.velocity = new Vector2(dir, 0); // 발판 우측으로 이동.

    }

    // 잠시 대기
    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
