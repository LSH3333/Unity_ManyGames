using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_CreateMap : MonoBehaviour
{
    private Vector3 flagLeft, flagRight;
    public GameObject column, roof;

    private void Start()
    {
        flagLeft = new Vector3(14.98f, -1.22f, 0f);
        flagRight = new Vector3(16.64f, -1.22f, 0f);
    }

    void SpawnRandomColumn()
    {
        // 가운데 두개의 기둥 
        Instantiate(column, flagLeft, Quaternion.identity);
        Instantiate(column, flagRight, Quaternion.identity);
        
        // 2~6개의 기둥 랜덤 소환 
        int cnt = Random.Range(2, 7);

        float left = flagLeft.x;
        float right = flagRight.x;
        for(int i = 1; i <= cnt; i++)
        {
            // 좌측소환 
            if(i % 2 != 0)
            {
                left -= 1.66f; // plank의 긴부분 길이 
                 
            }
            // 우측소환 
            else
            {

            }
        }

    }

}
