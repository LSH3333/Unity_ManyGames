using UnityEngine;
using UnityEngine.UI;

public class JumperManagerGame : ManagerParent
{
    public static JumperManagerGame singleton;

    [HideInInspector]
    public bool GameEnds;


    [SerializeField]
    public GameObject ResultBoard;
    private Rigidbody2D PlayerRB; // Player의 RIgidbody

    // RESULTBOARD SCORE
    [SerializeField]
    private Text YourScoreText;

    //float _score = 0.0f;

    public float Score
    {
        // 상속받은 ManagerParent의 score_float
        get { return score; }
    }

    // Sound
    public AudioSource gameover_audio;

    public GameObject Player;

    private void Awake()
    {
        singleton = this;

        // set resolution
        Screen.SetResolution(1080, 1920, false);

        GameEnds = false;
        //ResultBoard = GameObject.Find("ResultBoard");
        ResultBoard.SetActive(false);
        PlayerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
    }

    // 게임종료, Result보드 표시
    public void setGameOver()
    {
        if (GameEnds) return; // GameEnds가 true면 함수실행x

        GameEnds = true;
        ResultBoard.SetActive(true);

        score = (int)GameObject.Find("Player").GetComponent<Player>().topScore;
        YourScoreText.text = "Your Score: " + Mathf.Round(score).ToString();
        
        gameover_audio.Play(); // GameOver sound play

        Destroy(Player, 10f);
        //PlayerRB.bodyType = RigidbodyType2D.Kinematic; // Rigidbody 무력화
    }

}

