using UnityEngine;
using UnityEngine.SceneManagement;

public class ab_buttonmanager : MonoBehaviour
{
    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
