using UnityEngine;

// 1인칭 시점에서의 움직임 조정
public class PlayerMovementFristPersonView_NM : MonoBehaviour
{
    [SerializeField]
    private Camera cam; // 1인칭 시점 카메라 
    [SerializeField]
    private float cameraRotationLimit = 85f; 

    [SerializeField]
    private float speed = 5f; // 이동속도 
    private Rigidbody rb; // player의 rigidbody
    [SerializeField]
    private float mouseSensitivity = 3f; // 마우스 로테이션 감도 

    private float cameraRotation;
    private float currentCameraRotation;

    private void Awake()
    {
        this.enabled = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        if (GameManager_NM.singleton.gameMode != 1) return;

        // Moving
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 horMove = transform.right * hor;
        Vector3 verMove = transform.forward * ver;

        Vector3 velocity = (horMove + verMove).normalized * speed;
        Move(velocity);

        // Rotation
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * mouseSensitivity;

        // camera rotation
        float xRot = Input.GetAxisRaw("Mouse Y");
        cameraRotation = xRot * mouseSensitivity;

        Rotate(rotation);

    }

    void Move(Vector3 _velocity)
    {
        if(_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
        }
    }

    void Rotate(Vector3 _rotation)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));

        if(cam != null)
        {
            currentCameraRotation -= cameraRotation;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
        }
    }
}
