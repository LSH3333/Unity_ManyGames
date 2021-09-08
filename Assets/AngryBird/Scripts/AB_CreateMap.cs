using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_CreateMap : MonoBehaviour
{
    private Vector3 flagLeft, flagRight;
    private Vector3 flagRoof;
    private float PlankLongSide = 1.66f, PlankShortSide = 0.4f;
    public GameObject column, roof;

    private int max = 7;

    private void Start()
    {
        // 중앙 좌측 판자 
        flagLeft = new Vector3(14.98f, -1.22f, 0f);
        // 중앙 우측 판자 
        flagRight = new Vector3(16.64f, -1.22f, 0f);
        // 중앙 지붕 
        flagRoof = new Vector3(15.81f, -0.19f, 0f);        

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
                Debug.Log("leftPos:" + i + " " + leftPos.x + "," + leftPos.y);
            }
            // 우측소환 
            else
            {
                rightPos.x += PlankLongSide;
                Instantiate(column, rightPos, column.transform.rotation);

                rightRoofPos.x += PlankLongSide;
                Instantiate(roof, rightRoofPos, roof.transform.rotation);
                Debug.Log("rightPos:" + i + " " + rightPos.x + "," + rightPos.y);
            }
        }

        flagLeft.y += PlankLongSide + PlankShortSide;
        flagRight.y += PlankLongSide + PlankShortSide;
        flagRoof.y += PlankLongSide + PlankShortSide;
    }

}
