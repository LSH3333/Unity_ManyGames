using UnityEngine;
using MyEngine;

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
