using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicResourcesManager : MonoBehaviour
{
    public GameObject Intro;
    public GameObject ResultBoard;
    public GameObject FadeSystem;

    public GameObject Canvas;

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        InstantiateIntro();
    }

    // Canvas의 child로 Intro 프리팹 Instantiate 
    private void InstantiateIntro()
    {
        GameObject intro = Instantiate(Intro, Intro.transform.position, Quaternion.identity);
        intro.transform.parent = Canvas.transform;
        intro.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f);
    }

    // 게임 종료되는 시점에 호출됨. 
    public void SetGameOver()
    {
        GameObject board = Instantiate(ResultBoard, ResultBoard.transform.position, Quaternion.identity);
        board.transform.parent = Canvas.transform;
        board.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f);
        board.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }
}
