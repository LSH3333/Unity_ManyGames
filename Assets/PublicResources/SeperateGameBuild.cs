using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class SeperateGameBuild : MonoBehaviour
{
    public SceneControl sceneControl;

    // Start is called before the first frame update
    void Start()
    {
        ChangeSceneToGame("Nightmare");
    }

    public void ChangeSceneToGame(string gameName)
    {
        NCMBLogIn("guest", "guest", gameName);
    }

    public void NCMBLogIn(string name, string pw, string gameName)
    {
        NCMBUser.LogInAsync(name, pw, (NCMBException e) =>
        {
            // login 실패 
            if (e != null)
            {
                print("NCMB LogIn Failed");

            }
            else // login 성공
            {
                sceneControl.sendSelectGame(gameName);
                sceneControl.ChangeScene(gameName + "_Title");
            }
        });
    }

}
