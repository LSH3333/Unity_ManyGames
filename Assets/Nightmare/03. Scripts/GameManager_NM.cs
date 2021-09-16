using UnityEngine;
using UnityEngine.UI;

public class GameManager_NM : ManagerParent
{
    public static GameManager_NM singleton;

    public bool isGameOver = false;

    // 0: intro, 1: game play, 2: game result
    [HideInInspector] public int gameMode = 0;

    private void Awake()
    {
        singleton = this;

        // GameManager가 활성화되면 BestScore를 가져옴. 
        _txtBest = GameObject.Find("txtBest").GetComponent<Text>();
        GetBestScore();
    }

    // 게임오버. 결과창 띄움 
    public void SetGameOver()
    {
        isGameOver = true;
        // PublicResourcesManager에게 게임오버 알림 -> ResultBoard 소환됨 
        GameObject.Find("PublicResourcesManager").GetComponent<PublicResourcesManager>().SetGameOver();
    }

    
}
