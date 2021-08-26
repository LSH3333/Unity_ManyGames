using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEngine;
using UnityEngine.UI;

public class TextTestDLL : MonoBehaviour
{
    public Text txt;
    private MyEngine.TestDLL td;

    private void Start()
    {
        td = new MyEngine.TestDLL();

        td.ChangeText(txt);
        gameObject.AddComponent<Text>();
    }

    void MakeIntro()
    {
        gameObject.AddComponent<Text>();

    }

}
