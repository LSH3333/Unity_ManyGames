using NCMB;
using UnityEngine;
using UnityEngine.UI;

public class SignUpSystem : MonoBehaviour
{
    // Login canvas
    public GameObject cvLogIn;
    // SignUp canvas
    public GameObject cvSignUp;

    // SignUp 진행할 ID,PW InputField 
    public Text ifID;
    public Text ifPW;

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
            NCMBSignUp(ifID.text, ifPW.text);
        }
    }

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
                signUpFailPannel.GetComponent<CanvasFadeOut>().PanelFadeOut();
            }                
            else // signup 성공 
            {
                print("NCMB SignUp Success" + '\n' + "UserName: " + name + '\n' + "PW: " + pw);
                // signup 성공 알리는 panel 
                SignUpSuccessPanel.GetComponent<CanvasFadeOut>().PanelFadeOut();
                // signup 성공했으니 다시 login canvas로 이동 
                _loginSys.ActivateLogInCanvas();
            }
        });
    }

    // SignUp canvas에서 back 버튼 눌렀을시 LogIn canvas로 되돌아감 
    public void OnClickBackButton()
    {
        cvSignUp.SetActive(false);
        cvLogIn.SetActive(true);
    }

}
