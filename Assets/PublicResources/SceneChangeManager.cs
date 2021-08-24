using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    // 씬 전환시 Fading
    public SceneFading sceneFadeSys;

    public void NextScene(string sceneName)
    {
        sceneFadeSys.FadeToScene(sceneName);
        //SceneManager.LoadScene(sceneName);
    }
}
