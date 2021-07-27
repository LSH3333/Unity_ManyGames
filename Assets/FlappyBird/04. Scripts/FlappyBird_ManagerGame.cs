using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using NCMB;

public class FlappyBird_ManagerGame : ManagerParent
{
    public static FlappyBird_ManagerGame inst;
    [HideInInspector] public bool isGameOver = false;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    private GameObject _pGameOver;
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

    private void Awake()
    {
        inst = this;
        _txtScore = GameObject.Find("txtScore").GetComponent<Text>();
        
        _txtLife = GameObject.Find("txtLife").GetComponent<Text>();

        _pGameOver = GameObject.Find("boardResult");
        _pGameOver.SetActive(false);

        // BestScore 오브젝트는 부모에 선언되있음. (모든 게임들에서 쓰일것이기 때문에)
        // 게임매니저가 활성화되면 즉 게임이 시작되면 데이터베이스에서 bestscore를 가져옴. 
        // 새로운 게임을 추가하면, 
        // 1. Canvas의 자식으로 txtBest 게임오브젝트 추가.
        // 2. 해당 게임매니저에 Awake()문에 아래 두줄 추가. 

        // GameManager가 활성화되면 BestScore를 가져옴. 
        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore();
        
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
        score += 10;
        _txtScore.text = string.Format("Score : {0}", score);
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
    
    /*// 현재 게임의 "Score"를 정렬해서 가져와서 가장 높은 점수인 BestScore를 찾는다 
    void GetBestScore()
    {        
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ManageApp.singleton.gameName);
        query.AddDescendingOrder("Score");

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB get BestScore Failed");
            }
            else
            {
                _txtBest.text = "BestScore: ";
                int cnt = 0;
                // "Score"를 정렬해서 가져왔으므로 첫 원소가 가장 높은 점수 즉 BestScore이다.    
                foreach (NCMBObject obj in objList)
                {
                    _txtBest.text += obj["Score"];
                    cnt++;
                    if (cnt >= 1) break;
                }
            }
        });
    }*/
}
