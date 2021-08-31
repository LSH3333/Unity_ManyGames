using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_MapSpawnManager : MonoBehaviour
{
    // 소환할 WoodStructure 
    public GameObject woodStructurePrefab;
    public int enemiesDead = 0;

    private void Start()
    {
        SpawnWoodStructure();
        
    }

    // 3층으록 구성된 WoodStructure 소환 
    public void SpawnWoodStructure()
    {
        Instantiate(woodStructurePrefab, woodStructurePrefab.transform.position, Quaternion.identity);
    }

}
