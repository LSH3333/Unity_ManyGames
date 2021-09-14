using UnityEngine;
using UnityEngine.UI;
using System;

public class Angry_ManagerGame : ManagerParent
{
    public static Angry_ManagerGame singleton;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    public Text txtCurScore;

    private void Awake()
    {
        singleton = this;

        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore();
        
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

}

