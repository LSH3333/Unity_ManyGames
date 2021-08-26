using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyEngine;

public class IntroTest : MonoBehaviour
{
    private MyEngine.Intro intro;
    private int count = 3;
    Text txt;

    private void Start()
    {
        intro = new MyEngine.Intro();
        txt = GetComponent<Text>();    

    }

    public void ChangeCount2()
    {
        count = intro.ChangeCount(count, txt, gameObject);
    }
}
