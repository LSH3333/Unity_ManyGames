using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFunction : MonoBehaviour
{
    public GameObject _pGameOver;

    private void Start()
    {
        _pGameOver.SetActive(false);
    }

    public void setGameOver()
    {
        //Angry_ManagerGame.singleton.gameMode = 2;
        _pGameOver.SetActive(true);
        //Debug.Log("Gameover");
    }
}
