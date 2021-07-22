using UnityEngine;

public class CameraFollow_NM : MonoBehaviour
{
    public Transform target; // 카메라가 쫒을 대상(플레이어)
    
    public float smoothing = 5f; // 쫒아가는 속도

    private Vector3 offset;

    

    private void Start()
    {
     
        // 현재 카메라와 타겟의 물리적 거리를 기억해 둔다 
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // 타겟이 움직였고, 기억해둔 거리값을 이용해서 카메라의 새로운 위치값 계산
        Vector3 newPos = target.position + offset;
        // 카메라의 위치를 변경시키는데, 다소 부드럽게 새로운 위치값으로 이동하도록 처리
        transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
    }
}
