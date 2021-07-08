﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // "AngryBird_Button", "Jumper Button" ... 등 OnClick에 연결 
    // 해당 버튼의 OnClick에 저장된 string (씬이름)에 따라 씬 변경  
    public void ChangeScene(string _scenename)
    {
        SceneManager.LoadScene(_scenename);        
    }

    // 해당 버튼의 OnClick에 저장된 string (게임이름)을 ManagerApp 클래스의 selectGame 함수에 전달 
    // ManageApp의 Player_BsetScore, Player_Nickname..등을 해당 게임이름으로 바꿈. 
    public void sendSelectGame(string gamename)
    {
        GameObject.Find("AppManager").GetComponent<ManageApp>().selectGame(gamename);        
    }
}
