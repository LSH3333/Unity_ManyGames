using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_CreateMap : MonoBehaviour
{
    // 기준, 중앙 좌측 판자, 우측 판자 
    private Vector3 flagLeft, flagRight;
    // 기준, 중앙 지붕 판자 
    private Vector3 flagRoof;
    // 기준, 중앙 Bird 위치 
    private Vector3 flagBird;
    // 판자의 긴부분, 짧은부분 길이 
    private float PlankLongSide = 1.66f, PlankShortSide = 0.4f;
    // 소환할 프리펩 
    public GameObject column, roof, bird;

    // 해당 게임오브젝트의 자식으로 소환 
    public GameObject SpawnedObjects;

    public int enemiesDead = 0;

    private int max = 7;

    private void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        // 중앙 좌측 판자 
        flagLeft = new Vector3(14.98f, -1.22f, 0f);
        // 중앙 우측 판자 
        flagRight = new Vector3(16.64f, -1.22f, 0f);
        // 중앙 지붕 
        flagRoof = new Vector3(15.81f, -0.19f, 0f);
        // 중앙 Bird 
        flagBird = new Vector3(15.81f, -1.715f, 0f);

        // 1층 최대 7칸 
        max = 7;
        // 1st floor 
        SpawnRandomColumn();
        // 2nd floor 
        SpawnRandomColumn();
        // 3rd floor 
        SpawnRandomColumn();

    }

    void SpawnRandomColumn()
    {
        // 가운데 두개의 기둥 
        Instantiate(column, flagLeft, column.transform.rotation);        
        Instantiate(column, flagRight, column.transform.rotation);
        // 가운데 지붕 
        Instantiate(roof, flagRoof, roof.transform.rotation);

        // 2~6개의 기둥 랜덤 소환 
        int cnt = Random.Range(2, max);
        // 위층이 아래층 보다 작은 값 나오도록 
        max = cnt;
        
        // column 
        Vector3 leftPos = flagLeft;
        Vector3 rightPos = flagRight;
        // roof
        Vector3 leftRoofPos = flagRoof;
        Vector3 rightRoofPos = flagRoof;
        for (int i = 1; i <= cnt; i++)
        {
            // 좌측소환 
            if (i % 2 != 0)
            {
                // column 
                leftPos.x -= PlankLongSide;
                Instantiate(column, leftPos, column.transform.rotation);            

                // roof 
                leftRoofPos.x -= PlankLongSide;
                Instantiate(roof, leftRoofPos, roof.transform.rotation);
                
                //Debug.Log("leftPos:" + i + " " + leftPos.x + "," + leftPos.y);
            }
            // 우측소환 
            else
            {
                rightPos.x += PlankLongSide;
                Instantiate(column, rightPos, column.transform.rotation);
                

                rightRoofPos.x += PlankLongSide;
                Instantiate(roof, rightRoofPos, roof.transform.rotation);
               
                //Debug.Log("rightPos:" + i + " " + rightPos.x + "," + rightPos.y);
            }
        }

        // Bird 
        Vector3 birdPos = flagBird;
        int birdCnt = Random.Range(0, cnt + 1);
        if (birdCnt % 2 != 0)
        {
            birdPos.x -= (birdCnt/2+1) * PlankLongSide;
        }
        else
        {
            birdPos.x += (birdCnt / 2) * PlankLongSide;
        }
        Instantiate(bird, birdPos, bird.transform.rotation);
        

        // 판자들 y값 상승 (1층 위로) 
        flagLeft.y += PlankLongSide + PlankShortSide;
        flagRight.y += PlankLongSide + PlankShortSide;
        flagRoof.y += PlankLongSide + PlankShortSide;
        // Bird y값 상승 (1층 위로)
        flagBird.y += PlankLongSide + PlankShortSide;
    }

}
