using UnityEngine;

public class Firball : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    public float fireball_speed_y = 5f;
    public float fireball_speed_x = 0f;
    public GameObject explosion_effect; // Fireball 닿았을때 터지는 effect

    // Player's animator
    public Animator _ani;

    // Audio
    public AudioSource _audio;

    private void Start()
    {
        _rb2d.velocity = new Vector2(fireball_speed_x, fireball_speed_y);
        _ani = GameObject.Find("Player").GetComponent<Animator>();
        Destroy(gameObject, 10f); // Fireball 소환 후 일정시간 후 제거
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // GameOver 
        JumperManagerGame.singleton.setGameOver();
        FireballDestroy();
    }

    public void FireballDestroy()
    {
        //_audio.Play();
        // Fireball 터진 위치에 Effect 소환
        Instantiate(explosion_effect, transform.position, Quaternion.identity);
        //Destroy(gameObject); // Player와 닿으면 닿은 Fireball 소멸
        //_audio.Play();

        // audio, script 제외한 컴포넌트들 disable
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        _audio.Play(); // audio 재생
        Destroy(gameObject, 6f); // 일정시간 후 gameobject 파괴

    }



}
