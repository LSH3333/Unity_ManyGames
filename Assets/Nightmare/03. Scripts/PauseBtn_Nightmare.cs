using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn_Nightmare : PauseBtn
{
    public override void SetGamePause()
    {
        // pause -> play 
        if(GameManager_NM.singleton.gameMode == 3)
        {
            GameManager_NM.singleton.gameMode = 1;

            image.sprite = pause;
            goBackBtn.SetActive(false);
        }
        // play -> pause 
        else
        {
            GameManager_NM.singleton.gameMode = 3;

            image.sprite = play;
            goBackBtn.SetActive(true);
        }        
    }
}
