using UnityEngine;
using UnityEngine.UI;
using System;
using Management;

public class Angry_ManagerGame : Manage
{
    public static Angry_ManagerGame singleton;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    public Text txtCurScore;

    protected override void Awake()
    {
        singleton = this;
        base.Awake();
        Invoke("SetIntro", 1f);

        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore(ManageApp.singleton.gameName);
        
    }

    // 부모 ManagerParent 클래스의 score 
    public int Score
    {
        set { score = value; }
        get { return score;  }
    }


    public void UpdateCurScore()
    {
        txtCurScore.text = "Score: " + Convert.ToString(Angry_ManagerGame.singleton.score);
    }

    private void SetIntro()
    {
        InstantiateUI("Intro", "Canvas", false);
    }

    public override void SetStart()
    {
        Angry_ManagerGame.singleton.gameMode = 1;
    }

    public void SetGameOver()
    {
        Angry_ManagerGame.singleton.gameMode = 2;
        InstantiateUI("boardResult", "Canvas", false);
    }
}

