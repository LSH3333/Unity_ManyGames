using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEngine;


public class DLLTest : MonoBehaviour
{
    private MyEngine.CameraFollow cf;
    public Transform target;

    private void Start()
    {
        cf = new MyEngine.CameraFollow();
        
    }

    private void FixedUpdate()
    {
        cf.CameraFollowTarget(target, gameObject.transform, 5f, transform.position - target.position);
    }
}
