using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn_FlappyBird : PauseBtn
{
    public override void SetGamePause()
    {
        if (FlappyBird_ManagerGame.inst.gameMode == 0) return;

        // pause -> play 
        if (FlappyBird_ManagerGame.inst.gameMode == 3)
        {
            FlappyBird_ManagerGame.inst.gameMode = 1;
            GameObject.Find("Bird").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            image.sprite = pause;
            goBackBtn.SetActive(false);
        }
        // play -> pause 
        else
        {
            FlappyBird_ManagerGame.inst.gameMode = 3;
            GameObject.Find("Bird").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            image.sprite = play;
            goBackBtn.SetActive(true);
        }
    }

}
