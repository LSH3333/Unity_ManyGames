using NCMB;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SignUpSystem : MonoBehaviour
{
    // Login canvas
    public GameObject cvLogIn;
    // SignUp canvas
    public GameObject cvSignUp;

    // SignUp 진행할 ID,PW InputField 
    public Text ifID;
    //public Text ifPW;
    public InputField ifPW;

    public GameObject SignUpSuccessPanel;
    public LogInSystem _loginSys;

    public GameObject signUpFailPannel;

    // signup canvas의 SignUp 버튼 누를시 
    public void OnClickSignUpButton()
    {
        // 아이디와 비밀번호가 입력이 됐다면 
        if (ifID.text != "" && ifPW.text != "")
        {
            // sign up 
            if(ManageApp.singleton.DBtype == ManageApp.DB.NCMB)
            {
                NCMBSignUp(ifID.text, ifPW.text);
            }
            else if(ManageApp.singleton.DBtype == ManageApp.DB.PostgreSQL)
            {
                HttpSignUp(ifID.text, ifPW.text);
            }
                        
            ifID.GetComponentInParent<InputField>().text = "";
            ifID.text = "";
            ifPW.text = "";
        }
    }

    ///////////////// NCMB 
    void NCMBSignUp(string name, string pw)
    {
        NCMBUser user = new NCMBUser();        

        user.UserName = name;
        user.Password = pw;
        
        user.SignUpAsync((NCMBException e) =>
        {
            if (e != null) // signup 실패 
            {
                print("NCMB SignUp Failed, " + e.ErrorMessage);
                SignUpFail();
            }                
            else // signup 성공 
            {
                print("NCMB SignUp Success" + '\n' + "UserName: " + name + '\n' + "PW: " + pw);
                SignUpSuccessful();

                SendUserPrivateData(name, pw);
            }
        });
    }

    // 계정 생성 시 User Private Data 보냄 
    private void SendUserPrivateData(string username, string pw)
    {
        NCMBObject obj = new NCMBObject("UserPrivateData");
        obj.Add("UserName", username);
        obj.Add("AccountCreateDate", DateTime.Now.ToString());
        obj.Add("UserPw", pw);

        obj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
                Debug.Log("NCMB UserPrivateData send Failed");
            else
                Debug.Log("NCMB UserPrivateData send Success");
        });
    }

    ///////////////// HTTP 
    public void HttpSignUp(string name, string pw)
    {
        StartCoroutine(HttpSignUpIE(name, pw));
    }

    IEnumerator HttpSignUpIE(string name, string pw)
    {
        string url = ManageApp.singleton.Url + "member/register";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name", name));
        formData.Add(new MultipartFormDataSection("password", pw));

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, formData);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete");
            string response = unityWebRequest.downloadHandler.text;
            Debug.Log("response = " + response);
            // 가입 성공 
            if(response == "SUCCESS")
            {
                SignUpSuccessful();
            }
            else
            {
                SignUpFail();
            }
            
        }
    }



    // SignUp canvas에서 back 버튼 눌렀을시 LogIn canvas로 되돌아감 
    public void OnClickBackButton()
    {
        cvSignUp.SetActive(false);
        cvLogIn.SetActive(true);
    }

    private void SignUpSuccessful()
    {
        // signup 성공 알리는 panel 
        SignUpSuccessPanel.GetComponent<CanvasFadeOut>().PanelFadeOut();
        // signup 성공했으니 다시 login canvas로 이동 
        _loginSys.ActivateLogInCanvas();
    }

    private void SignUpFail()
    {
        signUpFailPannel.GetComponent<CanvasFadeOut>().PanelFadeOut();
    }
}
