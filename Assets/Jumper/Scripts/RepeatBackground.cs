using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField]
    private Transform centerBackground; // 3개의 배경중 가운데

    private void Update()
    {
        // 카메라의 y pos가 centerBackground y pos 보다 커지면
        // centerBackground의 위치를 옮김.
        if(transform.position.y >= centerBackground.position.y + 10.2f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y + 10.2f);
        }

        else if(transform.position.y <= centerBackground.position.y - 10.2f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y - 10.2f);
        }

    }
}
