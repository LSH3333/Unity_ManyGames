using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManagerControl : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void DestroyApp()
    {
        Destroy(GameObject.Find("AppManager"));
        Debug.Log("destroy appmanager");
    }
}
