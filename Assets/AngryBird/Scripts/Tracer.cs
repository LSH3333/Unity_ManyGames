using UnityEngine;

public class Tracer : MonoBehaviour {
    private SpringJoint2D spring;

    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
    }

    private void Update()
    {
        Vector2 dist = (Vector2)transform.position - spring.connectedAnchor;

        // 움직이는 바운스 상황에서의 distance 크기값 출력
        print(dist.magnitude);
    }
}
