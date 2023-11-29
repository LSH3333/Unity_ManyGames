using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using UnityEngine.Networking;

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

        
        // 메뉴씬 진입시 로그인이 되있는 상황이라면  (다른 게임에서 back으로 메뉴씬으로 넘어온경우) 
        if (NCMBUser.CurrentUser != null || ManageApp.singleton.loginNickName != null)
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

    private void Start()
    {


    }

    

    public void OnClickLogIn()
    {        
        // 아이디와 비밀번호가 입력이 됐다면 
        if (ifID.text != "" && ifPW.text != "")
        {            
            if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
            {
                // NCMB LogIn 
                NCMBLogIn(ifID.text, ifPW.text);
            }
            else
            {
                // Http Login
                HttpLogin(ifID.text, ifPW.text);
            }

          

            ifID.GetComponentInParent<InputField>().text = "";
            ifPW.text = "";
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

    /// HTTP 
    public void HttpLogin(string name, string password)
    {
        StartCoroutine(HttpLoginIE(name, password));
    }

    IEnumerator HttpLoginIE(string name, string password)
    {
        string url = ManageApp.singleton.Url + "member/login";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name", name));
        formData.Add(new MultipartFormDataSection("password", password));

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, formData);
        yield return unityWebRequest.SendWebRequest();
        
        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else // 성공 
        {
            Debug.Log("Form upload complete");
            string response = unityWebRequest.downloadHandler.text;
            Debug.Log("response = " + response);            

            // 가입 성공 
            if (response == "SUCCESS")
            {
                LoginSuccess();
                // ManageApp 에 로그인한 유저 name 저장 
                ManageApp.singleton.loginNickName = name;
                //ManageApp.singleton.DBtype = ManageApp.DB.PostgreSQL;
                // 서버에서 받은 jsessionid 저장 
                ManageApp.singleton.Jsessionid = GetJsessionId(unityWebRequest);
            }
            else
            {
                LoginFail();
            }

        }
    }

    // header에서 key=Set-Cookie 에 jsessionid 담겨있다 
    private string GetJsessionId(UnityWebRequest unityWebRequest)
    {
  /*      Dictionary<string, string> header = unityWebRequest.GetResponseHeaders();
        foreach (var x in header)
        {
            Debug.Log("key,val = " + x.Key + ',' + x.Value);
        }*/

        string cookie = unityWebRequest.GetResponseHeader("Set-Cookie");

        string[] cookieParts = cookie.Split(';');

        string jsessionId = null;
        foreach (string part in cookieParts)
        {
            if (part.Trim().StartsWith("jsessionid="))
            {
                jsessionId = part.Trim().Substring("jsessionid=".Length);
                break;
            }
        }
        return jsessionId;
    }

    //////////
    private void LoginSuccess()
    {
        // login한 계정이 운영자라면 
        if (name == "Admin")
        {
            AdminObj.SetActive(true);
        }

        cvLogIn.SetActive(false); // login canvas 비활성화
        cvLogOut.SetActive(true); // logout canvas 활성화
        slideGameObject.SetActive(true); // slide 활성화
    }

    private void LoginFail()
    {
        logInFailPannel.GetComponent<CanvasFadeOut>().PanelFadeOut();
    }
}
