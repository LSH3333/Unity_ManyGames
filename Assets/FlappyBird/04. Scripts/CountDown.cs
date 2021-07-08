using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private Text textCount;
    private int count;

    private void Start()
    {
        count = 3;
        textCount = GetComponent<Text>();
    }

    // animation에 사용될 변수
    public void ChangeCount()
    {
        count--; // count down
        textCount.text = (count >= 0) ? "" + count : "GO"; // 3, 2, 1, GO

        if(count < -1) // GO 이후 이제 게임 시작
        {
            FlappyBird_ManagerGame.inst.gameMode = 1; // Game Play Mode로 변경
            GameObject.Find("GameManager").GetComponent<ObstaclePool>().InitColumnCreate(); // 장애물 생성
            GameObject.Find("Bird").SendMessage("GameStart"); // 버드의 RIgidbody 활성화

            // tag가 HorzScroll인 모든 게임오브젝트 찾음
            GameObject[] objs = GameObject.FindGameObjectsWithTag("HorzScroll");
            foreach (var o in objs) // 횡스크롤 시작
                o.SendMessage("GameStart");

            Destroy(gameObject); // count down Text Object를 메모리에서 제거.
        }
    }
}
