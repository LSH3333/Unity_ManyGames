using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class LogInSystem : MonoBehaviour
{
    // login canvas 
    public GameObject cvLogIn;
    public Text ifID;    
    public InputField ifPW;

    // logout canvas 
    public GameObject cvLogOut;
    public Text welcome;

    // signup canvas
    public GameObject cvSignUp;

    // LogIn Fail Pannel
    public GameObject logInFailPannel;

    public LogOutSystem scriptLogOutSystem;

    // Slide Gameobject
    public GameObject slideGameObject;

    public GameObject AdminObj;

    private void Awake()
    {
        // 최초에는 login canvas가 활성화되어야함 
        //cvLogIn.SetActive(true);        
        //cvLogOut.SetActive(false);
        //cvSignUp.SetActive(false);

        //Testncmb();
    }

    private void Start()
    {
        // 메뉴씬 진입시 로그인이 되있는 상황이라면  (다른 게임에서 back으로 메뉴씬으로 넘어온경우) 
        if(NCMBUser.CurrentUser != null)
        {
            // logout canvas 활성화 
            cvLogOut.SetActive(true);
            cvLogIn.SetActive(false);
            cvSignUp.SetActive(false);
        }
        else
        {
            // 최초에는 login canvas가 활성화되어야함 
            cvLogIn.SetActive(true);        
            cvLogOut.SetActive(false);
            cvSignUp.SetActive(false);
        }

        
    }

    

    public void OnClickLogIn()
    {        
        // 아이디와 비밀번호가 입력이 됐다면 
        if (ifID.text != "" && ifPW.text != "")
        {
            
            // NCMB LogIn 
            NCMBLogIn(ifID.text, ifPW.text);            
        }
    }

    // login canvas의 signup 버튼 눌렀을시 
    public void OnClickSignUp()
    {
        cvLogIn.SetActive(false);
        cvSignUp.SetActive(true);

    }

    public void ActivateLogInCanvas()
    {
        cvLogIn.SetActive(true);
        cvLogOut.SetActive(false);
        cvSignUp.SetActive(false);
    }

    public void NCMBLogIn(string name, string pw)
    {
        NCMBUser.LogInAsync(name, pw, (NCMBException e) =>
        {
            // login 실패 
            if (e != null)
            {
                print("NCMB LogIn Failed");
                // login 실패 pannel
                logInFailPannel.GetComponent<CanvasFadeOut>().PanelFadeOut();
            }                
            else // login 성공
            {
                // login한 계정이 운영자라면 
                if(name == "Admin")
                {
                    AdminObj.SetActive(true);
                }

                print("NCMB LogIn Success, " + name);
                cvLogIn.SetActive(false); // login canvas 비활성화
                cvLogOut.SetActive(true); // logout canvas 활성화
                slideGameObject.SetActive(true); // slide 활성화
                welcome.text = "Welcome " + NCMBUser.CurrentUser.UserName;
            }
        });
    }

}
