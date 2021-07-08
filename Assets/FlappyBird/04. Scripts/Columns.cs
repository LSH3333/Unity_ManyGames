using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Columns : MonoBehaviour
{
    private BoxCollider2D _box;

    private void Start()
    {
        _box = gameObject.GetComponent<BoxCollider2D>();
        _box.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Bird>() != null)
        {
            FlappyBird_ManagerGame.inst.SetAddScore();
        }
    }
}
