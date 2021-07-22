using UnityEngine;

// 카메라 1인칭, 3인칭 시점으로 변경하면서
// 1인칭일때는 3인칭 스크립트 비활성화
// 3인칭일때는 1인칭 스크립트 비활성화
public class PlayerViewManager_NM : MonoBehaviour
{
    public PlayerMovement_NM movementScript; // 3rd person view 
    public PlayerMovementFristPersonView_NM firstPersonViewScript; // 1st person view 
    public Camera firstPersonViewCam;
  

    private void Update()
    {
        CamSwitch();
    }

    // Space 누르면 시점 1인칭 <-> 3인칭 변경  
    void CamSwitch()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (firstPersonViewCam.enabled)
            {
                firstPersonViewCam.enabled = false;
                firstPersonViewScript.enabled = false;
                movementScript.enabled = true;

            }
            else
            { // change to FirstPersonView 
                firstPersonViewCam.enabled = true;
                firstPersonViewScript.enabled = true;
                movementScript.enabled = false;

            }

        }
    }
}
