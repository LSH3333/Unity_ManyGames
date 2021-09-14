using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_CameraMoveTo : MonoBehaviour
{
    private Vector3 initCamPos;
    private Vector3 nextCamPos;
    public bool toInit = false;
    public bool trigger = false;
    public bool updateTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        initCamPos = new Vector3(0, 0, -10);
        nextCamPos = new Vector3(16, 0, -10);
        updateTrigger = true;
    }

    private void Update()
    {
        if (!updateTrigger || Angry_ManagerGame.singleton.gameMode == 2) return;

        if (transform.position.x >= 15.5f && !trigger)
        {
            trigger = true;
            toInit = true;
        }
        if(transform.position.x <= 0.5f && toInit)
        {
            updateTrigger = false;
        }

        if(toInit)
            Camera.main.transform.position = Vector3.Lerp(transform.position, initCamPos, Time.deltaTime);
        else
            Camera.main.transform.position = Vector3.Lerp(transform.position, nextCamPos, Time.deltaTime);
    }

}
