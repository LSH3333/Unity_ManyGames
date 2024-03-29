﻿using UnityEngine;
using UnityEngine.SceneManagement;
using NCMB;

public class SceneControl : MonoBehaviour
{


    // "AngryBird_Button", "Jumper Button" ... 등 OnClick에 연결 
    // 해당 버튼의 OnClick에 저장된 string (씬이름)에 따라 씬 변경  
    public void ChangeScene(string _scenename)
    {
        // NCMB
        if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
        {
            // login안된 상태라면 
            if (NCMBUser.CurrentUser == null)
            {
                // Please enter nickname panel   
                GameObject.Find("NoNicknamePanel").GetComponent<CanvasFadeOut>().PanelFadeOut();
            }
            else
            {
                // 게임버튼 클릭할때 현재 로그인된 유저정보 ManageApp에 전달됨 
                ManageApp.singleton.NickName = NCMBUser.CurrentUser.UserName;
                ManageApp.singleton.loginNickName = NCMBUser.CurrentUser.UserName;
                GameObject.FindGameObjectWithTag("GameManager").SendMessage("SetFadeout", _scenename);
            }
        }
        // PostgreSQL
        else
        {
            if(ManageApp.singleton.loginNickName == null)
            {
                GameObject.Find("NoNicknamePanel").GetComponent<CanvasFadeOut>().PanelFadeOut();
            }
            else
            {
                ManageApp.singleton.NickName = ManageApp.singleton.loginNickName;
                GameObject.FindGameObjectWithTag("GameManager").SendMessage("SetFadeout", _scenename);
            }
        }


        
    }

    // 해당 버튼의 OnClick에 저장된 string (게임이름)을 ManagerApp 클래스의 selectGame 함수에 전달 
    // ManageApp의 Player_BsetScore, Player_Nickname..등을 해당 게임이름으로 바꿈. 
    public void sendSelectGame(string gamename)
    {        
        GameObject.Find("AppManager").GetComponent<ManageApp>().selectGame(gamename);        
    }

    public void OnClickButtonAnimationTrigger()
    {
        GetComponent<Animator>().SetTrigger("OnClick");
    }
}
