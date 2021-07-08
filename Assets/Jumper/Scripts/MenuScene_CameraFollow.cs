using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene_CameraFollow : MonoBehaviour
{
    public Transform targetToFollow;

    private void Update()
    {
        // 카메라가 Player를 따라감, 하지만 Player가 다시 아래로 이동했을때는 따라가지 않음.
        transform.position = new Vector3(
            transform.position.x, // 나의 x pos
            targetToFollow.position.y , // Player의 y pos
            transform.position.z);
    }
}
