using UnityEngine;
using UnityEngine.UI;
using Management;

public class FlappyBird_ManagerGame : Manage
{
    public static FlappyBird_ManagerGame inst;
    [HideInInspector] public bool isGameOver = false;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    private Text _txtScore, _txtLife;
    //private int _score = 0;
    private int _life = 3;

    [HideInInspector]
    public bool CheckBirdBlinking; // get from Bird class.

    public int Life
    {
        get { return _life; }
    }

    public int Score
    {
        // 상속받은 Managerparent의 변수 
        get { return score; }
    }

    protected override void Awake()
    {
        inst = this;
        _txtScore = GameObject.Find("txtScore").GetComponent<Text>();        
        _txtLife = GameObject.Find("txtLife").GetComponent<Text>();

        // fade 
        base.Awake();
        Invoke("SetIntro", 1f);

        // BestScore 오브젝트는 부모에 선언되있음. (모든 게임들에서 쓰일것이기 때문에)
        // 게임매니저가 활성화되면 즉 게임이 시작되면 데이터베이스에서 bestscore를 가져옴. 
        // 새로운 게임을 추가하면, 
        // 1. Canvas의 자식으로 txtBest 게임오브젝트 추가.
        // 2. 해당 게임매니저에 Awake()문에 아래 두줄 추가. 

        // GameManager가 활성화되면 BestScore를 가져옴. 
        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore(ManageApp.singleton.gameName);
        
    }

    // Intro 클래스에서 sendmessage로 호출됨 
    public override void SetStart()
    {
        // Game Play Mode로 변경
        FlappyBird_ManagerGame.inst.gameMode = 1;
        // Bird 움직임 시작 
        GameObject.Find("Bird").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // 장애물 생성 시작 
        GameObject.Find("GameManager").GetComponent<ObstaclePool>().InitColumnCreate();        

        // tag가 HorzScroll인 모든 게임오브젝트 찾음
        GameObject[] objs = GameObject.FindGameObjectsWithTag("HorzScroll");
        foreach (var o in objs) // 횡스크롤 시작
            o.SendMessage("GameStart");
    }

    public void SetGameOver()
    {
        FlappyBird_ManagerGame.inst.gameMode = 2; // 결과창모드로 전환
        isGameOver = true;        
        InstantiateUI("boardResult", "Canvas", false);
    }

    private void SetIntro()
    {
        InstantiateUI("Intro", "Canvas", false);
    }

    // 점수 추가, UI에 표시
    public void SetAddScore()
    {
        // cant score when bird is blinking 
        if (CheckBirdBlinking) return;
        score += 10;
        _txtScore.text = string.Format("Score : {0}", score);
    }

    // Bird 라이프 감소, UI에 표시
    public void SetLifeDown()
    {
        _life--;
        _txtLife.text = string.Format("Life : {0}", _life);
    }

}
