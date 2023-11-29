using NCMB;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LogOutSystem : MonoBehaviour
{
    // Login, Logout canvas 
    public GameObject cvLogIn, cvLogOut;

    // LogOut success pannel
    public GameObject _logoutSuccessPannel;

    public Text welcome;

    // Slide Gameobject
    public GameObject slideGameObject;

    public GameObject AdminObj;

    private void Awake()
    {
        SetSlideActive();
    }

    private void Start()
    {
        if (ManageApp.singleton.DBtype == ManageApp.DB.PostgreSQL)
        {
            welcome.text = "Welcome " + ManageApp.singleton.loginNickName;
        }
        else
        {
            welcome.text = "Welcome " + NCMBUser.CurrentUser.UserName;
        }
    }

    // 게임에서 메인메뉴씬으로 돌아왔을때 slider가 active되있도록 
    void SetSlideActive()
    {
        // NCMB login 상태라면 
        if(NCMBUser.CurrentUser != null || ManageApp.singleton.loginNickName != null)
        {
            slideGameObject.SetActive(true);
        }
    }

    // Logout canvas에서 LogOut 버튼 눌렀을시 
    public void OnClickLogOutButton()
    {
        if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
        {
            NCMBLogOut();
        }
        else
        {
            HttpLogout();
        }

        // Admin obj unactive 
        AdminObj.SetActive(false);
    }

    // 현재 로그인되어있는 계정 로그아웃 
    public void NCMBLogOut()
    {
        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e != null)
                print("Logout failed " + e.ErrorMessage);
            else
            {
                print("Logout successed");
                LogoutSuccess();
            }
                
        });
    }


    private void HttpLogout()
    {
        StartCoroutine(HttpLogoutIE());
    }

    IEnumerator HttpLogoutIE()
    {
        string url = ManageApp.singleton.Url + "member/logout";

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else // 성공 
        {
            Debug.Log("Http Logout Success");
            ManageApp.singleton.Jsessionid = null; // session 파기 
            LogoutSuccess();
        }
    }


    private void LogoutSuccess()
    {
        // Logout success pannel
        _logoutSuccessPannel.GetComponent<CanvasFadeOut>().PanelFadeOut();

        // Login canvas로 되돌아감 
        cvLogOut.SetActive(false);
        cvLogIn.SetActive(true);
        slideGameObject.SetActive(false); // slide 사라짐 
    }
}
