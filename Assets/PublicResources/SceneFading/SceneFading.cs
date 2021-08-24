using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFading : MonoBehaviour
{
    public Animator animator;
    // Fade 완료후 전환할 씬이름 
    private string sceneToLoad;

 
    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("SceneFadeOut");
    }

    // Fade가 완료된후 scene load 
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    
}
