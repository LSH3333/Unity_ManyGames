using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRankingBox : MonoBehaviour
{
    public void OnClickBack()
    {
        GetComponent<Animator>().SetTrigger("popout");        
        Destroy(gameObject, 1f);
    }
}
