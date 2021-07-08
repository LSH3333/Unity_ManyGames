using UnityEngine;

public class Plank : MonoBehaviour {
    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _rb2d.AddForce(Vector2.right * 200);
        }
    }
}
