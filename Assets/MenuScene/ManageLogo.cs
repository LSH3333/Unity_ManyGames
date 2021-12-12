using UnityEngine;
using Management;

public class ManageLogo : Manage
{
    protected override void Awake()
    {
        // 부모 클래스인 Manage의 함수를 호출 
        base.Awake();
        Invoke("SetFadeout", 2);
    }

    void SetFadeout()
    {
        SetFadeout("MenuScene");
    }

    public override void SetStart()
    {
    }

}
