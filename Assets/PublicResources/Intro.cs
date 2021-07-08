using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private int count;
    public Text _text;

    private void Start()
    {
        count = 3;
        _text = GetComponent<Text>();
    }

    public void ChangeCount()
    {
        count--;
        _text.text = (count >= 0) ? "" + count : "GO";

        if(count < -1)
        {
            // Intro는 여러가지 게임씬에서 사용될것이기 때문에 특정 이름의 게임오브젝트를 찾아서 함수를 실행하기 부적절함.
            // 따라서 Intro 실행 후 실행해야할 함수가 있는 GameObject에 Intro 태그를 달고, 
            // tag가 Intro인 GameObject 찾아서 IntroEnds 함수 실행

            // GameObject with "Intro" tags:
            // Jumper : Player
            // AngryBird : IntroControl
            GameObject LetStart = GameObject.FindGameObjectWithTag("Intro");        
            LetStart.SendMessage("IntroEnds");
            

            Destroy(gameObject);
        }
    }

    
}
