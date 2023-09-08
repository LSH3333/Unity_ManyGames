using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class PauseBtn : MonoBehaviour
{
    public Sprite pause, play;
    public Image image;
    public GameObject goBackBtn;


    // 각 미니게임 마다 일시정지 로직 다르다 
    public abstract void SetGamePause();

}
