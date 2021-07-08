using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlappyBird_ManagerGame : MonoBehaviour
{
    public static FlappyBird_ManagerGame inst;
    [HideInInspector] public bool isGameOver = false;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    private GameObject _pGameOver;
    private Text _txtScore, _txtBest, _txtLife;
    private int _score = 0, _life = 3;

    [HideInInspector]
    public bool CheckBirdBlinking; // get from Bird class.

    public int Life
    {
        get { return _life; }
    }

    public int Score
    {
        get { return _score; }
    }

    private void Awake()
    {
        inst = this;
        _txtScore = GameObject.Find("txtScore").GetComponent<Text>();
        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        _txtLife = GameObject.Find("txtLife").GetComponent<Text>();

        //_pGameOver = GameObject.Find("txtGameOver");
        _pGameOver = GameObject.Find("boardResult");
        _pGameOver.SetActive(false);

        _txtBest.text = string.Format("Bestscore : {0}", ManageApp.singleton.BestScore);

    }




    private void Update()
    {
        //Debug.Log(CheckBirdBlinking);
    }


    public void SetGameOver()
    {
        FlappyBird_ManagerGame.inst.gameMode = 2; // 결과창모드로 전환
        gameMode = 2;
        isGameOver = true;
        _pGameOver.SetActive(true);
    }

    // 점수 추가, UI에 표시
    public void SetAddScore()
    {
        // cant score when bird is blinking 
        if (CheckBirdBlinking) return;
        _score += 10;
        _txtScore.text = string.Format("Score : {0}", _score);
    }

    // Bird 라이프 감소, UI에 표시
    public void SetLifeDown()
    {
        _life--;
        _txtLife.text = string.Format("Life : {0}", _life);
    }

    // 씬 전환
    public void NextScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
