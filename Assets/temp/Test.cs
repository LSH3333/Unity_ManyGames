using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public BaseMonster[] basemonster;

    private void Start()
    {
        for(int i = 0; i < basemonster.Length; i++)
        {
            Debug.Log(basemonster[i].gameObject.name);
        }

    }


}
