using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEngine;


public class CameraFollowDLL : MonoBehaviour
{
    private MyEngine.CameraFollow cf;    
    public Transform target;
    public Transform cam;
    private Vector3 offset;

    private void Start()
    {
        cf = new MyEngine.CameraFollow();        
        offset = transform.position - target.position;
        cam = gameObject.transform;
    }

    private void FixedUpdate()
    {        
        //transform.position = cf.CameraFollowTarget(target, gameObject, 5f, offset);

        cf.CameraFollowTarget(target, ref cam, 5f, offset);
    }
}
