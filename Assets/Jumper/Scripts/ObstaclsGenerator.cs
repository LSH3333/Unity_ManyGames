using UnityEngine;
using System.Collections;
using System.Collections.Generic;
enum FireballDirection
{
    Downward,
    RightToLeft,
    LeftToRight
};

// Spawn Fireball
public class ObstaclsGenerator : MonoBehaviour
{
    public GameObject pre_fireball_upward; // Fireball Prefab
    public GameObject pre_fireball_downward;
    public GameObject pre_fireball_RightToLeft;
    public GameObject pre_warn;


    public Transform CameraPos; // Main Camera Transform Position


    // vertical (downward)
    public float maxTime = 5f;
    public float minTime = 2f;

    public float levelWidth = 10f;
    Vector3 spawnPosition = new Vector3();
    private float time; // current time
    private float spawnTime;


    // horizontal
    public float hor_maxTime = 5f;
    public float hor_minTime = 2f;

    public float hor_levelWidth = 5f;
    Vector3 hor_spawnPosition = new Vector3();
    private float hor_time;
    private float hor_spawnTime;

    // 소환된 파이어볼들 
    private List<GameObject> spawned_fireballs = new List<GameObject>();


    private void Start()
    {
        SetRandomTime(FireballDirection.Downward);
        SetRandomTime(FireballDirection.RightToLeft);
        time = minTime;
        hor_time = hor_minTime;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime; // 시간 가는중...
        hor_time += Time.deltaTime;
        //Debug.Log("hor_spawnTime:: " + hor_spawnTime);

        if (hor_time >= hor_spawnTime && !JumperManagerGame.singleton.GameEnds)
        {
            StartCoroutine(SpawnFireball_horizontal());
            SetRandomTime(FireballDirection.RightToLeft);
        }

        // 시간이 지정된 spawnTime에 도달하면 소환, Game이 진행중일때만
        if (time >= spawnTime && !JumperManagerGame.singleton.GameEnds)
        {
            //SpawnFireball_upward();

            StartCoroutine(SpawnFireball_downward());

            SetRandomTime(FireballDirection.Downward); // 다음 spawnTime 다시 랜덤으로 설정
        }
    }


    public void StopAllFireballs()
    {
        foreach(var fireball in spawned_fireballs)
        {
            fireball.GetComponent<Firball>().StopMoving();
        }
    }

    public void MoveAllFireballs()
    {
        foreach (var fireball in spawned_fireballs)
        {
            fireball.GetComponent<Firball>().StartMoving();
        }
    }


    // Random x pos, Fireball 소환 (아래에서 위로)
    void SpawnFireball_upward()
    {
        time = 0;
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        spawnPosition.y = CameraPos.transform.position.y - 10f;

        Instantiate(pre_fireball_upward, spawnPosition, Quaternion.identity);
    }
    /*
    // (위에서 아래로)
    void SpawnFireball_downward()
    {
        time = 0;
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        spawnPosition.y = CameraPos.transform.position.y + 10f;

        Instantiate(pre_fireball_downward, spawnPosition, Quaternion.identity);
    }
    */

    IEnumerator SpawnFireball_downward()
    {
        time = 0;
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        spawnPosition.y = CameraPos.transform.position.y + 10f;

        yield return StartCoroutine(SpawnWarn()); // warning 후
        yield return StartCoroutine(Spawn()); // Fireball Spawn

    }

    IEnumerator SpawnFireball_horizontal()
    {
        hor_time = 0;
        hor_spawnPosition.x = CameraPos.transform.position.x + 7f;
        hor_spawnPosition.y = Random.Range(CameraPos.transform.position.y - hor_levelWidth, CameraPos.transform.position.y + hor_levelWidth);

        yield return StartCoroutine(hor_SpawnWarn());
        yield return StartCoroutine(hor_Spawn());

    }

    // Random 시간 설정 (spawnTime)
    void SetRandomTime(FireballDirection dir)
    {
        switch (dir)
        {
            case FireballDirection.Downward:
                spawnTime = Random.Range(minTime, maxTime);
                break;

            case FireballDirection.RightToLeft:
                hor_spawnTime = Random.Range(hor_minTime, hor_maxTime);
                break;
        }

    }

    IEnumerator Spawn()
    {
        spawned_fireballs.Add(Instantiate(pre_fireball_downward, spawnPosition, Quaternion.identity));
        yield return null;
    }

    IEnumerator hor_Spawn()
    {
        spawned_fireballs.Add(Instantiate(pre_fireball_RightToLeft, hor_spawnPosition, pre_fireball_RightToLeft.transform.rotation));
        yield return null;
    }

    // warning!
    IEnumerator SpawnWarn()
    {
        Instantiate(pre_warn, new Vector2(spawnPosition.x, spawnPosition.y - 5.5f), Quaternion.identity);
        yield return null;
    }

    IEnumerator hor_SpawnWarn()
    {
        Instantiate(pre_warn, new Vector2(hor_spawnPosition.x, hor_spawnPosition.y), Quaternion.identity);
        yield return null;
    }

}
