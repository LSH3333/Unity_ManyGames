using NCMB;
using UnityEngine;
using UnityEngine.UI;

public class LogOutSystem : MonoBehaviour
{
    // Login, Logout canvas 
    public GameObject cvLogIn, cvLogOut;

    // LogOut success pannel
    public GameObject _logoutSuccessPannel;

    public Text welcome;

    private void Start()
    {
        welcome.text = "Welcome " + NCMBUser.CurrentUser.UserName;
    }

    // Logout canvas에서 LogOut 버튼 눌렀을시 
    public void OnClickLogOutButton()
    {
        NCMBLogOut();
        
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

                // Logout success pannel
                _logoutSuccessPannel.GetComponent<CanvasFadeOut>().PanelFadeOut();

                // Login canvas로 되돌아감 
                cvLogOut.SetActive(false);
                cvLogIn.SetActive(true);
            }
                
        });
    }
}
