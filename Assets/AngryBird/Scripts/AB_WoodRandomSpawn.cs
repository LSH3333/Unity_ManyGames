using UnityEngine;


// 스크립트 폐기 (사용안함)

// 층 별로 1개~7개 woodSet 소환 
public class AB_WoodRandomSpawn : MonoBehaviour
{
    // WoodStructure 
    public GameObject[] woodFloor1, woodFloor2, woodFloor3;    
    private int maxSpawnNumOfWoods;

    // Bird
    public GameObject[] EnemyBird1, EnemyBird2, EnemyBird3;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InitFloors();
    }

    // 최초 시작시 모든 woodFloor, EnemyBird들 비활성 
    private void InitMap()
    {
        /*foreach (var x in woodFloor1)
            x.SetActive(false);
        foreach (var x in woodFloor2)
            x.SetActive(false);
        foreach (var x in woodFloor3)
            x.SetActive(false);
*/
        for(int i = 0; i < 7; i++)
        {
            woodFloor1[i].SetActive(false);
            woodFloor2[i].SetActive(false);
            woodFloor3[i].SetActive(false);

            EnemyBird1[i].SetActive(false);
            EnemyBird2[i].SetActive(false);
            EnemyBird3[i].SetActive(false);
        }    

    }

    public void InitFloors()
    {
        InitMap();

        maxSpawnNumOfWoods = 7;
        // 1층은 최대 7칸 소환, 2층은 1층이 랜덤으로 소환된 칸수 보다 적은수로 소환 
        InitFloor(woodFloor1, EnemyBird1);
        InitFloor(woodFloor2, EnemyBird2);
        InitFloor(woodFloor3, EnemyBird3);
    }

    // 각 층당 몇 칸 소환할지 
    private void InitFloor(GameObject[] woodFloor, GameObject[] enemyBird)
    {
        maxSpawnNumOfWoods = Random.Range(1, maxSpawnNumOfWoods);

        for(int i = 0; i <= maxSpawnNumOfWoods; i++)
        {
            woodFloor[i].SetActive(true);            
        }
        SpawnEnemyBird(enemyBird);
        //Debug.Log("NumOfWoods: " + maxSpawnNumOfWoods);
    }

    // 각 층, 랜덤 칸에 한마리의 EnemyBird 소환 
    private void SpawnEnemyBird(GameObject[] enemyBird)
    {
        int spawnIdx = Random.Range(0, maxSpawnNumOfWoods - 1);
        enemyBird[spawnIdx].SetActive(true);
        enemyBird[spawnIdx].GetComponent<BoxCollider2D>().enabled = true;
    }


}
