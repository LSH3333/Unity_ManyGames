using UnityEngine;
using Management;

// 이 스크립트를 PublicResourcesManager 프리팹에 부착.
// 각 게임 씬에서 Intro, ResultBoard, FadeSystem을 프리팹 하나로 관리.
// PublicResourcesManager 프리팹을 각 게임씬의 메모리에 올리고 
// currentSceneName, mainSceneName만 수정해주면 된다.
public class PublicResourcesManager : Manage
{
    // 현재 씬, 메인씬 이름. 하이라키에서 입력 
    // ResultBoard의 button에 전달 
    public string currentSceneName, mainSceneName;

    public GameObject Intro;
    public GameObject ResultBoard;
    public GameObject FadeSystem;
    private GameObject Canvas;

    protected override void Awake()
    {
        // fade 
        base.Awake();

        Canvas = GameObject.Find("Canvas");
        InstantiateIntro();
    }

    // Fadein FadeOut System 
    //private void InstantiateFadeSystem()
    //{
    //    GameObject fadeSys = Instantiate(FadeSystem, transform.position, Quaternion.identity);
    //    fadeSys.name = "SceneFadeSystem";
    //}

    // Canvas의 child로 Intro 프리팹 Instantiate 
    private void InstantiateIntro()
    {
        InstantiateUI("Intro", "Canvas", false);
    }

    // 게임 종료되는 시점에 호출됨. 
    // 각 게임씬에서 게임이 종료되는 시점에 호출하도록함. 
    public void SetGameOver()
    {        
        // ResultBoard 소환 
        InstantiateUI("boardResult", "Canvas", true);
    }

    public override void SetStart()
    {        
    }
}
