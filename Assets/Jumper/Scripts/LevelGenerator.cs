using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int numberOfPlatforms = 100;
    public float levelWidth = 2.2f;
    public float minY = .4f;
    public float maxY = .8f;

    public GameObject Plat_black, Plat_red, Plat_blue;
    private int PlatColorIndex;

    Vector3 spawnPosition = new Vector3();


    public GameObject platforms;

    private void Start()
    {  
        GeneratePlatforms(); // 시작할때 발판 생성
    }

    // 발판 생성. 
    public void GeneratePlatforms()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            PlatColorIndex = Random.Range(0, 4); // black, black, red, blue // black이 더큰확률

            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            GameObject spawned = null;
            switch (PlatColorIndex)
            {
                case 0: // black
                    spawned = Instantiate(Plat_black, spawnPosition, Quaternion.identity);                    
                    break;

                case 1: // black
                    spawned = Instantiate(Plat_black, spawnPosition, Quaternion.identity);
                    break;

                case 2: // red
                    spawned = Instantiate(Plat_red, spawnPosition, Quaternion.identity);
                    break;

                case 3: // blue
                    spawned = Instantiate(Plat_blue, spawnPosition, Quaternion.identity);
                    break;

                default:
                    break;
            }
            spawned.transform.SetParent(platforms.transform);
        }
    }


    private List<GameObject> GetAllSpawnedPlatforms()
    {
        List<GameObject> spawned = new List<GameObject>();
        for(int i = 0; i < platforms.transform.childCount; i++)
        {
            GameObject child = platforms.transform.GetChild(i).gameObject;
            spawned.Add(child);
        }
        return spawned;
    }

    public void StopAllPlatforms()
    {
        List<GameObject> spawned = GetAllSpawnedPlatforms();
        foreach(var platform in spawned)
        {
            platform.GetComponent<Bounce>().StopMoving();
        }
    }

    public void RestartAllPlatforms()
    {
        List<GameObject> spawned = GetAllSpawnedPlatforms();
        foreach (var platform in spawned)
        {
            platform.GetComponent<Bounce>().RestartMoving();
        }
    }
}
