using UnityEngine;

public class Angry_ManagerGame : ManagerParent
{
    public static Angry_ManagerGame singleton;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    public GameObject EnemyBird;
    //public int killedBirds = 0;

    private void Awake()
    {
        singleton = this;
    }

    // 부모 ManagerParent 클래스의 score 
    public int Score
    {
        set { score = value; }
        get { return score;  }
    }


    private void Update()
    {
        //Debug.Log(killedBirds);
    }

    public void setGameOver()
    {
        //Angry_ManagerGame.singleton.gameMode = 2;
        //_pGameOver.SetActive(true);
        Debug.Log("Gameover");
    }
}

