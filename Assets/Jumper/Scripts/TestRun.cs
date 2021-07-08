using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    public Animator _ani;

    private void Start()
    {
        _ani.SetTrigger("SetRun");
    }
}
