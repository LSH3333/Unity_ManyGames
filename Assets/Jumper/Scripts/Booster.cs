using UnityEngine;

public class Booster : MonoBehaviour
{
    // Player's rigidbody
    public Rigidbody2D _r2bd;
    public float speed; 
    public float acceleration; // 매초, 이 수치만큼 speed가 증가.
    public float speed_limit = 1000;
    public bool booster_trigger; // triggered by script:SwordBehaviour
    public SpriteRenderer boosterRenderer;
    public AudioSource boosterSound;

    private void Start()
    {
    }


    private void Update()
    {
        if(booster_trigger) // starting booster
        {
            Debug.Log("Booster!!");
            // disable player's collider (부스터 중 무적)
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            boosterRenderer.enabled = true; // enable booster effect

            // play boosterSound only when it is not playing
            if(!boosterSound.isPlaying) boosterSound.Play();

            Boosting();
            
        }
    }

    private void Boosting()
    {
        speed += Time.deltaTime * acceleration;
        _r2bd.velocity = Vector3.up * speed;
        
        if(speed >= speed_limit) // ends booster
        {
            booster_trigger = false;
            speed = 0;
            // Booster 끝난 후 collider 다시 active
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            boosterRenderer.enabled = false; // disable booster effect
            boosterSound.Stop(); // booster sound stop
        }
    }
        
}
