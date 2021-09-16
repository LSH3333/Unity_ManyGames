using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager_NM : MonoBehaviour
{
    public SpawnMoster_NM spawnMonster;

    // Intro 끝난후 몬스터 소환시작 
    public void IntroEnds()
    {
        spawnMonster.enabled = true;
        GameManager_NM.singleton.gameMode = 1; // game play mode 

    }
}
