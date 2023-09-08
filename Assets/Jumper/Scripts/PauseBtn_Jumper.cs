using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn_Jumper : PauseBtn
{
    public override void SetGamePause()
    {
        // pause -> play 
        if(JumperManagerGame.singleton.gameMode == 2)
        {
            JumperManagerGame.singleton.gameMode = 1;
            GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().enabled = true;
            JumperManagerGame.singleton.PlayerRB.bodyType = RigidbodyType2D.Dynamic;
            GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().MoveAllFireballs();
            GameObject.Find("GameManager").GetComponent<LevelGenerator>().RestartAllPlatforms();

            image.sprite = pause;
            goBackBtn.SetActive(false);
        }
        // play -> pause 
        else
        {
            JumperManagerGame.singleton.gameMode = 2;
            GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().enabled = false;
            JumperManagerGame.singleton.PlayerRB.bodyType = RigidbodyType2D.Static;
            GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().StopAllFireballs();
            GameObject.Find("GameManager").GetComponent<LevelGenerator>().StopAllPlatforms();

            image.sprite = play;
            goBackBtn.SetActive(true);
        }
        
    }
}
