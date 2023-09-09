using UnityEngine;
using Management;
using NCMB;

public class ManageLogo : Manage
{
    protected override void Awake()
    {
        // 부모 클래스인 Manage의 함수를 호출 
        base.Awake();
        NCMBLogOutWhenGameStart();

        Invoke("SetFadeout", 2);
    }

    void SetFadeout()
    {
        SetFadeout("MenuScene");
    }

    public override void SetStart()
    {
    }

    // 게임 시작시 로그아웃 
    public void NCMBLogOutWhenGameStart()
    {
        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e != null)
                print("Logout failed " + e.ErrorMessage);
            else
            {
                print("Logout successed");
            }

        });
    }



}
