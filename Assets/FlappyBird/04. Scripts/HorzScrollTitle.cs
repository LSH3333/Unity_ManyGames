using UnityEngine;
using HorzTools;

[RequireComponent(typeof(Rigidbody2D))]
public class HorzScrollTitle : HScroll
{
    private void Start()
    {
        setRigidbody(2f);
    }

}
