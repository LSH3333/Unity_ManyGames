using UnityEngine;

public class GameManager_NM : ManagerParent
{
    public static GameManager_NM singleton;
    // 결과창 
    public GameObject _pGameOver;
    public bool isGameOver = false;

    private void Awake()
    {
        singleton = this;

        // 결과창 게임 시작시에는 꺼놓음  
        _pGameOver = GameObject.Find("boardResult");
        _pGameOver.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log("NM Score: " + score);
    }

    // 게임오버. 결과창 띄움 
    public void SetGameOver()
    {
        isGameOver = true;
        _pGameOver.SetActive(true);
    }
}
