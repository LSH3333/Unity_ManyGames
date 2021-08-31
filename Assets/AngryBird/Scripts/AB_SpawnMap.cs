using UnityEngine;

// 층 별로 1개~7개 woodSet 소환 
public class AB_SpawnMap : MonoBehaviour
{
    public GameObject[] woodFloor1, woodFloor2, woodFloor3;
    //private Transform floor1Pos, floor2Pos, floor3Pos;
    private int maxSpawnNumOfWoods;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InitFloors();
    }

    // 최초 시작시 모든 woodFloor들 비활성 
    private void InitMap()
    {
        foreach (var x in woodFloor1)
            x.SetActive(false);
        foreach (var x in woodFloor2)
            x.SetActive(false);
        foreach (var x in woodFloor3)
            x.SetActive(false);
    }

    public void InitFloors()
    {
        InitMap();

        maxSpawnNumOfWoods = 7;
        // 1층은 최대 7칸 소환, 2층은 1층이 랜덤으로 소환된 칸수 보다 적은수로 소환 
        InitFloor(woodFloor1);
        InitFloor(woodFloor2);
        InitFloor(woodFloor3);
    }

    private void InitFloor(GameObject[] woodFloor)
    {
        maxSpawnNumOfWoods = Random.Range(1, maxSpawnNumOfWoods);

        for(int i = 0; i <= maxSpawnNumOfWoods; i++)
        {
            woodFloor[i].SetActive(true);
        }
        Debug.Log("NumOfWoods: " + maxSpawnNumOfWoods);
    }


}
