using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_NM : MonoBehaviour
{
    // 소환할 몬스터들 
    public GameObject _hellephant;
    public GameObject _zombear;
    public GameObject _zombunny;

    // timer
    private float time;
    private float spawnTime;
    public float minTime = 2f;
    public float maxTime = 4f;

    // spawn position
    public GameObject[] spawnPos;

    private void Start()
    {
        SetRandomTime();
        time = minTime;
    }

    private void Update()
    {
        // 시간흐른다 
        time += Time.deltaTime;
        
        if(time >= spawnTime)
        {
            // spawn random monster at random position 
            SpawnRandomMonster();


            time = 0; // 타이머 초기화 
            SetRandomTime(); // 다음 소환시간 설정 
        }
    }

    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    void SpawnRandomMonster()
    {
        // 소환할 몬스터의 인덱스 
        int MonsterIdx = Random.Range(0, 3);
        // 소환할 위치의 인덱스 
        int SpawnPosIdx = Random.Range(0, 5);

        switch(MonsterIdx)
        {
            case 0:
                Instantiate(_hellephant, spawnPos[SpawnPosIdx].transform.position, Quaternion.identity);
                break;

            case 1:
                Instantiate(_zombear, spawnPos[SpawnPosIdx].transform.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(_zombunny, spawnPos[SpawnPosIdx].transform.position, Quaternion.identity);
                break;
        }
    }
}
