using UnityEngine;

public class workflow : MonoBehaviour
{
    /*
     게임시작되면 타이틀씬에서 ManageTitle.cs와 ManageApp.cs가 있음.
    이름입력하면 ManageTitle.cs의 onClickName() 함수에 의해 ManageApp.cs의 nickName변수가 이름으로 저장됨.  
    이때 특정게임으로 넘어가면서 ManageApp.cs의 selectGame() 함수에 의해 Load()가 먼저 호출되므로 
    nickName이 "none"이 되버림. 따라서 tempNickname에 저장해 놨다가 Save()에서 NickName = tempNickName으로 해줌. 

    여러 게임들중 하나를 선택하면 SceneControl.cs의 sendSelectGame() 함수에의해 ManageApp.cs의 selectGame() 함수로 게임이름을 string으로 전달.
    ManageApp.cs의 selectGame() 함수는 전달받은 게임이름을 바탕으로 player_BestScore, player_Nickname... 등 변수들을 게임이름에 맞게 변경함. 

    이제 특정 게임이 선택됐고 player_BestScore, player_Nickname등 변수들 이름도 게임에 맞게 변경되었으므로 Load()해서 해당 게임의 정보들을 가져오고 Save()해서 tempNickName에 있던 입력받은 닉네임도 바르게 설정함.

    ---- 게임끝나고 랭킹창
    Result.cs의 SetResult() 함수
    
    ____________________________________
    이 위로 모두 폐기됨. 
    NCMB Database 사용함에 따라 PlayerPref 사용하는 랭킹시스템 모두 폐기. 
    ____________________________________

    1. 메뉴씬에서 로그인
    2. 로그인 하지 않으면 게임버튼 누를수 없음
    3. 로그인후 게임버튼 누르면 SceneControl.cs의 ChangeScene() 함수에의해 singleton인 ManageApp.cs에 loginNickname 저장됨.
    4. 게임 종료후 Result.cs 에서 현재 로그인된 닉네임을 포함해서 NCMB Database에 정보 보냄.
       (여기서 정보는 UserManageMent와 상관없이 DataStore로 보내짐)
        즉 로그인 정보는 UserManageMent에 저장되지만 랭킹시스템에 출력되는 모든 정보들은 DataStore에 따로 관리.
    5. BestScore는 ManagerParent.cs에서 관리. 각 GameManager들은 ManagerParent를 상속받음 


    -------- NCMB Database 관련
    각각의 게임들에는 개별적으로 **_ManagerGame.cs, **_Result.cs 가 있음. (Angry_ManagerGame, Angry_Result 등...)
    이번에 실행된 게임의 점수는 ManagerGame.cs의 Score에 저장됨.
    게임이 종료되면 Result.cs가 Start되고 SendPlayerDataNCMB() 함수에 의해 NCBM 데이타베이스로 현재 종료된 게임의 이름과 점수가 보내짐.
    바로 직후 InitNCMBBoard() 함수에 의해 데이터베이스에 저장된 모든 데이터들을 "Score"로 정렬해 가져와서 보드에 출력함.



    */
}
