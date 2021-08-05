using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwordMan : MonoBehaviour
{
    public Rigidbody2D rigid;

    private void Start()
    {
        rigid.bodyType = RigidbodyType2D.Kinematic;
        rigid.velocity = new Vector3(-1, 0, 0);
    }


}
