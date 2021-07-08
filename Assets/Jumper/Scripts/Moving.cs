using UnityEngine;

public class Moving : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private int dir = 1;

    private void Start()
    {
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       
        MovingPlatforms();
    }

    void MovingPlatforms()
    {
        // 화면 양쪽 끝 도달시 방향 반대
        if (transform.position.x >= 2.3f || transform.position.x <= -2.3f) dir *= -1;
        _rb2d.velocity = new Vector2(dir, 0); // 발판 우측으로 이동.
    }
}
