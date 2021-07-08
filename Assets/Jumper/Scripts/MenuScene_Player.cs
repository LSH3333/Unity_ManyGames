using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene_Player : MonoBehaviour
{
    public Rigidbody2D MenuScenePlayer_r2bd;
    public float speed;


    private void Start()
    {
        
    }

    private void Update()
    {
        MenuScenePlayer_r2bd.velocity = Vector3.up * speed;
    }

}
