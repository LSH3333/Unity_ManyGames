using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MainMenuBird : MonoBehaviour
{
    public float upForce = 4f;

    private Rigidbody2D _rb2d;
    private Animator _anim;

    public float speed = -0.02f;
    private float timer = 0f;
    public float flapCycle = 1.6f; // 이 시간에 한번씩 Bird가 flap함 

    private Vector3 movement;

    public Transform target;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>(); // Attached된 Animator 컴포넌트
        _rb2d.bodyType = RigidbodyType2D.Dynamic;

        _rb2d.velocity = new Vector3(-1, 0, 0);

    }

    private void FixedUpdate()
    {
        //Movement();
        BirdJump();
    }

    private void Movement()
    {
        movement.Set(speed, 0f, 0f);
        _rb2d.MovePosition(transform.position + movement);

        
    }

    private void BirdJump()
    {
        // timer 흐름 
        timer += Time.deltaTime;

        // flapCycle에 한번씩 flap 
        if (timer >= flapCycle)
        {
            //_rb2d.velocity = Vector2.zero;
            _rb2d.velocity = new Vector3(-1, 0, 0);
            _rb2d.AddForce(new Vector2(0f, upForce));
            _anim.SetTrigger("SetFlap"); // flap animation
            timer = 0; // timer 초기화 
        }
    }
}
