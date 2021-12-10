using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Management;

public class ManageMenu : Manage
{
    public Text textName, textBscore, textBtnName, textIF;
    
    public GameObject pInputField;
    public bool status = false; // InputField 상태값, show/hide

    public string onClickStart_string, onClickBack_string;

    // fade 
    protected override void Awake()
    {
        base.Awake();
    }

    

    private void Start()
    {
        /*  랭킹시스템 메인메뉴씬으로 빼는경우 필요없음.
        // 로드된 값들을 바탕으로 UI상의 문자열을 변경한다
        textName.text = string.Format(
            "NICKNAME : {0}", ManageApp.singleton.NickName); // singleton property

        textBscore.text = string.Format(
            "BEST SCORE : {0}", ManageApp.singleton.BestScore); // singleton property
        */
        // InputField는 시작함과 동시에 안보이게 처리
        pInputField.SetActive(false);

        //OnMenuSceneStarted();
    }

    // "Name" 버튼 클릭 핸들러
    public void onClickName()
    {
        // 로그인이 된 상태라면        
        if(ManageApp.singleton.loginNickName != "")
        {
            // InputFiled 안뜨도록함
            return;
        }

        status = !status; // InputField의 상태값 전환        
        textBtnName.text = (status) ? "Okay" : "Login"; // 상태값에 따라 버튼 문자열 변경
        pInputField.SetActive(status); // status 상태에 따라 전환

        // 닉네임 입력 후 버튼을 다시 클릭하면 status값이 false가 되고 InputField에 입력한
        // 문자열이 저장되는 방식.
        if (status == false && textIF.text != "")
        {
            // 입력된 nickname 문자열을 저장 처리
            ManageApp.singleton.NickName = textIF.text;
            // 씬이 바뀌면서 Load() 호출되면서 nickName이 none으로 초기화되므로 
            // ManageApp의 tempNickName에 잠시 이름 저장했다가 나중에 다시 바꿔줌 
            ManageApp.singleton.loginNickName = textIF.text;

            // UI 상의 nickname 문자열을 업데이트
            textName.text = string.Format("nickname : {0}", textIF.text);
            textIF.text = "";
        }
    }

    // Start 버튼 클릭 핸들러
    public void onClickStart()
    {
        SceneManager.LoadScene(onClickStart_string);
    }

    public void onClickBack()
    {
        SceneManager.LoadScene(onClickBack_string);
    }

    // 메뉴씬이 시작되면 로그인된 닉네임으로 textName.text가 표시되도록함 
    public void OnMenuSceneStarted()
    {       
        string loggedInName = ManageApp.singleton.loginNickName;
        textName.text = string.Format("nickname : {0}", loggedInName);
    }


    // 게임시작 아니므로 body만 구현 
    public override void SetStart()
    {        
    }
}
