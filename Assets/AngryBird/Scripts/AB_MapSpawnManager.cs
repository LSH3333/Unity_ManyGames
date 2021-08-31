using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_MapSpawnManager : MonoBehaviour
{
    public GameObject woodStructurePrefab;
    

    private void Start()
    {
        //SpawnWoodStructure();
        
    }

    public void SpawnWoodStructure()
    {
        Instantiate(woodStructurePrefab, transform.position, Quaternion.identity);
    }

}
