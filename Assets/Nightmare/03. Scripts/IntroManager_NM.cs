using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager_NM : MonoBehaviour
{
    public SpawnMoster_NM spawnMonster;

    public void IntroEnds()
    {
        spawnMonster.enabled = true;
    }
}
