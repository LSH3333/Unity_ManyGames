using UnityEngine;
using MyEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorzScrollTitle : HScroll
{
    private void Start()
    {
        setRigidbody(2f);
    }

}
