using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper_IntroControl : MonoBehaviour
{
    public GameObject player;

    // Called after Intro Ends
    public void IntroEnds()
    {
        JumperManagerGame.singleton.gameMode = 1;
        // 게임 시작하면서 Player의 Rigidbody 활성 
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;        
        // Fireball 소환 시작.
        GameObject.Find("GameManager").GetComponent<ObstaclsGenerator>().enabled = true;
    }
}
