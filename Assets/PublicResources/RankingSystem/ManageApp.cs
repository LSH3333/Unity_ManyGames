using UnityEngine;
using System;

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
            //Load();
            DontDestroyOnLoad(gameObject);
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }
        
    }


    // _names[], _scores[]에 값들 가져옴 
    private void Load()
    {
        //Debug.Log("LOAD()");
        bestScore = PlayerPrefs.GetInt(player_BestScore, 0);
        nickName = PlayerPrefs.GetString(player_Nickname, "none");
        //Debug.Log("player_NickName : " + player_Nickname);
        //Debug.Log("nickName : " + nickName);

        string scores = PlayerPrefs.GetString(player_Scores, _defaultScores);
        string names = PlayerPrefs.GetString(player_Names, _defalutNames);

        //_scores = new int[10];
        //_names = new string[10];

        // names set     
        string[] tmps = names.Split(',');
        string[] tmpi = scores.Split(',');
        for(int i = 0; i < 10; i++)
        {
            _names[i] = tmps[i];
            _scores[i] = Convert.ToInt32(tmpi[i]);
        }

        Debug.Log("_names[]: " + _names);
        
    }

    public void Save()
    {
        // 타이틀씬에서 입력받은 이름으로 
        NickName = loginNickName;
        // BestScore 저장 
        PlayerPrefs.SetInt(player_BestScore, bestScore);
        PlayerPrefs.SetString(player_Nickname, nickName);

        //_scores = new int[10];
        //_names = new string[10];

        string scores = "" + _scores[0];
        string names = _names[0];
        // 콤마로 나눠서 저장 
        for(int i = 1; i < 10; i++)
        {
            scores += "," + _scores[i];
            names += "," + _names[i];
        }
        PlayerPrefs.SetString(player_Scores, scores);
        PlayerPrefs.SetString(player_Names, names);
    }

    public void SetData(int index, string name, int score)
    {
        _names[index] = name;
        _scores[index] = score;
    }

    // out: 참조를 통해 인자를 전달 가능. 2개의 데이터를 반환해야하기 때문에 return대신 out 키워드 사용
    public void GetData(int index, out string out_name, out int out_score)
    {
        out_name = _names[index];
        out_score = _scores[index];
    }

    // _names[], _scores[] 에 저장된 정보들을 가져와서 하나의 string 형태로 만들어 리턴한다 
    public string getRankString()
    {
        string res = "";

        Load();
        

        for(int i = 0; i < 10; i++)
        {
            res += string.Format("{0:D2}. {1} ({2:#,0})\n",
                i + 1, _names[i], _scores[i]);
        }
        return res;
    }

    public void updateBestScore(int score)
    {
        bestScore = (bestScore < score) ? score : bestScore;
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
        Load();

        Save();
    }

    
    private void Update()
    {
        /*Debug.Log(player_Names);
        Debug.Log("PlayerPrefs:" + PlayerPrefs.GetString(player_Names));*/

        //Debug.Log(nickName);
    }
    
}
