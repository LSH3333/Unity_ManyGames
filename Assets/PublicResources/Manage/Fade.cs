using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private string _nextScene = "MenuScene";


    public void endFadeIn()
    {
        gameObject.SetActive(false);
    }

    public void endFadeOut()
    {
        SceneManager.LoadScene(_nextScene);
    }

    public void setFadeOut()
    {
        GetComponent<Animator>().SetTrigger("SetFadeout");
    }

    public void setNextScene(string nextScene)
    {
        _nextScene = nextScene;
    }
}
