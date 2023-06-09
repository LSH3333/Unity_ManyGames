using UnityEngine;
using UnityEngine.UI;
using Management;

public class GameManager_NM : Manage
{
    public static GameManager_NM singleton;
    public SpawnMoster_NM spawnMonster;

    [HideInInspector]
    public bool isGameOver = false;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    protected override void Awake()
    {
        singleton = this;
        base.Awake();
        Invoke("SetIntro", 1f);

        // GameManager가 활성화되면 BestScore를 가져옴. 
        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore(ManageApp.singleton.gameName);
    }

    public override void SetStart()
    {
        spawnMonster.enabled = true;
        GameManager_NM.singleton.gameMode = 1; // game play mode 
    }

    // 게임오버. 결과창 띄움 
    public void SetGameOver()
    {
        isGameOver = true;
        GameManager_NM.singleton.gameMode = 2;
        InstantiateUI("boardResult", "Canvas", false);
    }
   
    private void SetIntro()
    {
        InstantiateUI("Intro", "Canvas", false);
    }
}
