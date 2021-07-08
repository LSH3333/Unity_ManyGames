using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    private GameObject DestroyTarget;

    private void Start()
    {
        
    }

    private void Awake()
    {
        // 메뉴로 다시 돌아왔을때 DontDestroyOnLoad의 AppManager 제거. (다른 게임 진입시 랭킹시스템 겹침)
        DestroyTarget = GameObject.Find("AppManager");
        Destroy(DestroyTarget);
    }
}
