using UnityEngine.UI;
using UnityEngine;

public class LogOutButton : MonoBehaviour
{
    public Text textName;

    public void LogOut()
    {        
        ManageApp.singleton.loginNickName = "";
        textName.text = string.Format("nickname : {0}", ManageApp.singleton.loginNickName);

        GameObject.Find("TitleManager").GetComponent<ManageMenu>().status = false; 
    }
}
