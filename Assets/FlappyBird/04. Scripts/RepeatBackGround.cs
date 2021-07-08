using UnityEngine;
using HorzTools;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatBackGround : HRepeat
{
    private void Start()
    {
        setBoxCollider(); 
    }

    private void Update()
    {
        updateObject();
    }
}
