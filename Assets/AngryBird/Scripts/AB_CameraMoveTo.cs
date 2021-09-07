using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_CameraMoveTo : MonoBehaviour
{
    private Vector3 initCamPos;
    private Vector3 nextCamPos;
    private bool toInit = false;
    private bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        initCamPos = new Vector3(0, 0, -10);
        nextCamPos = new Vector3(16, 0, -10);        
    }

    private void Update()
    {
        if (transform.position.x >= 15.5f && !trigger)
        {
            trigger = true;
            toInit = true;
        }

        if(toInit)
            Camera.main.transform.position = Vector3.Lerp(transform.position, initCamPos, Time.deltaTime);
        else
            Camera.main.transform.position = Vector3.Lerp(transform.position, nextCamPos, Time.deltaTime);
    }

}
