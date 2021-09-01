using UnityEngine;

public class CameraFollowBomb : MonoBehaviour {

    public int num_target = 3; // target Bomb의 갯수
    //public GameObject[] targetToFollow;
    public GameObject[] targetToFollow; // Camera가 따라갈 target

    // SpawnMap script     
    public AB_MapSpawnManager spawnWood;
    public GameObject[] bombLeft;
    public int bombLeftIdx = 0;

    public int CurTargetIdx = 0; // Current Bomb Index

    void Start()
    {
        
    }

    void Update()
    {   /*
        // Bomb의 Spring이 Destroy됐다 = 공이 날라가기 시작함
        if(GameObject.Find("Bomb").GetComponent<Bomb>().SpringDestroy)
        {
            // 카메라가 target을 따라감.
            transform.position = new Vector3(
                targetToFollow.transform.position.x,
                targetToFollow.transform.position.y,
                transform.position.z);
        }
        */
        // spring이 파괴되면
        //if (targetToFollow[CurTargetIdx] == null) return;
        if (CurTargetIdx >= 3)
        {
            GameObject newBomb = GameObject.Find("Bomb(Clone)");
            Debug.Log("FollowFound: " + newBomb);
            if (newBomb.GetComponent<Bomb>().SpringDestroy)
            {
                transform.position = new Vector3(
                newBomb.transform.position.x,
                newBomb.transform.position.y,
                transform.position.z);
            }
        }
        else if (targetToFollow[CurTargetIdx].GetComponent<Bomb>().SpringDestroy)
        {
            transform.position = new Vector3(
                targetToFollow[CurTargetIdx].transform.position.x,
                targetToFollow[CurTargetIdx].transform.position.y,
                transform.position.z);
        }

        
    }

    // 현재 Bomb의로 카메라 일시적으로 이동
    public void SwitchBomb()
    {
        // 다음 Bomb으로 넘어갈때마다 Canvas의 Bomb_left를 disable.
        //GameObject.Find("Canvas").transform.GetChild(CurTargetIdx).gameObject.SetActive(false);

        // bombLeftIdx >= 3이라면 3개의 목숨을 다 썼다는 의미 
        if(bombLeftIdx >= 3 && spawnWood.enemiesDead != 3)
        {
            GameObject.Find("GameManager").GetComponent<GameOverFunction>().setGameOver();
        }

        // 3마리 다 잡지 못했다면 목숨 감소
        if(spawnWood.enemiesDead != 3 && bombLeftIdx < 3)
        {
            bombLeft[bombLeftIdx++].SetActive(false);
        }

        //CurTargetIdx = (++CurTargetIdx) % 3;
        CurTargetIdx = ++CurTargetIdx;
        //Debug.Log("CurTargetidx: " + CurTargetIdx);
        // 카메라 초기 위치로 이동
        transform.position = new Vector3(
            0f, 0f, transform.position.z);

        
    }
}
