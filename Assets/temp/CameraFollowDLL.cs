using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEngine;


public class CameraFollowDLL : MonoBehaviour
{
    private MyEngine.CameraFollow cf;    
    public Transform target;
    private Vector3 offset;

    private void Start()
    {
        cf = new MyEngine.CameraFollow();
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {        
        transform.position = cf.CameraFollowTarget(target, gameObject, 5f, offset);
        
    }
}
