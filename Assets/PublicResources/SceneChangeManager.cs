using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    // 씬 전환시 Fading
    public SceneFading sceneFadeSys;

    // 버튼에서 참조 
    public void NextScene(string sceneName)
    {
        sceneFadeSys.FadeToScene(sceneName);        
    }
}
