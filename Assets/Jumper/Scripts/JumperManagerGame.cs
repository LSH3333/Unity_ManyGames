using UnityEngine;
using UnityEngine.UI;
using Management;
public class JumperManagerGame : Manage
{
    public static JumperManagerGame singleton;

    [HideInInspector]
    public bool GameEnds;
    // 0: intro, 1: game play, 2: game result
    [HideInInspector]
    public int gameMode = 0;

    [SerializeField]
    public GameObject ResultBoard;
    private Rigidbody2D PlayerRB; // Player의 RIgidbody

    // RESULTBOARD SCORE
    [SerializeField]
    private Text YourScoreText;

    //float _score = 0.0f;

    //public float Score
    //{
    //    // 상속받은 ManagerParent의 score_float
    //    get { return score; }
    //}

    // Sound
    public AudioSource gameover_audio;

    public GameObject Player;

    protected override void Awake()
    {
        singleton = this;
        base.Awake();
        Invoke("SetIntro", 1f);

        //GameEnds = false;

        PlayerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        // GameManager가 활성화되면 BestScore를 가져옴. 
        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore(ManageApp.singleton.gameName);
    }

    public override void SetStart()
    {
        GameEnds = false;
        JumperManagerGame.singleton.gameMode = 1;
        // 게임 시작하면서 Player의 Rigidbody 활성 
        GameObject.Find("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // Fireball 소환 시작.
        GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().enabled = true;
    }

    // 게임종료, Result보드 표시
    public void setGameOver()
    {
        if (GameEnds) return; // GameEnds가 true면 함수실행x
        JumperManagerGame.singleton.gameMode = 2;
        GameEnds = true;

        score = (int)GameObject.Find("Player").GetComponent<Player>().topScore;
        //YourScoreText.text = "Your Score: " + Mathf.Round(score).ToString();
        
        gameover_audio.Play(); // GameOver sound play
        InstantiateUI("boardResult", "Canvas", false);
        Destroy(Player, 10f);
        //PlayerRB.bodyType = RigidbodyType2D.Kinematic; // Rigidbody 무력화
    }

    private void SetIntro()
    {
        InstantiateUI("Intro", "Canvas", false);
    }

    
}

