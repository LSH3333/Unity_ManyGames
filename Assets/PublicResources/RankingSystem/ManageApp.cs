using UnityEngine;
using System;
using NCMB;

public class ManageApp : MonoBehaviour
{
    public static ManageApp singleton;

    private string nickName;
    public string loginNickName;

    private int bestScore;

    private int[] _scores = new int[10];
    private string[] _names = new string[10];

    private string _defaultScores = "0,0,0,0,0,0,0,0,0,0";
    private string _defalutNames = "N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A";

    public string gameName;
    public string player_BestScore, player_Nickname, player_Scores, player_Names;

    public LogOutSystem scriptLogOutSystem;

    public int BestScore // property
    {
        get { return bestScore; }
        set { bestScore = value;  }
    }

    public string NickName // property
    {
        get { return nickName; }
        set { nickName = value;  }
    }

    private void Awake()
    {
        // singleton
        if(singleton == null)
        {
            singleton = this;            
            DontDestroyOnLoad(gameObject);
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }

        NCMBLogOutWhenGameStart();
    }

    private void Start()
    {

        //NCMBLogOutWhenGameStart();
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
   

 

    public void selectGame(string gamename)
    {
        gameName = gamename;
        player_BestScore = gamename + "_Bestscore";
        player_Nickname = gamename + "_Nickname";
        player_Scores = gamename + "_Scores";
        player_Names = gamename + "_Names";

        // Title에서 ManageTitle.cs에 의해 현재 클래스 ManageApp의 맴버변수 nickName이 변경됨.
        // 그런데 타이틀씬에서 게임씬으로 넘어오면서 Load() 함수가 호출되서 nickName이 none으로 초기화되버림.
        // 그래서 selectGame함수가 호출되서 게임씬으로 넘어올때 Load()를 해주고 그 다음에 바로 Save() 다시해줌.

        // 게임이 선택된 후에, player_BsetScore, player_Nickname등이 해당 게임이름으로 바뀐 후에 Load함.        
        //Load();

        //Save();
    }
   
    
}
