using UnityEngine;

public class angry_Bird : MonoBehaviour {
	private Animator _anims;
	private BoxCollider2D _col;

    public float health = 4f;
    public GameObject EffectDie;

    public static int EnemiesAlive = 0;

    public AB_MapSpawnManager spawnManager;
    private int maxCnt = 0;

    void Start()
    {
		_anims = GetComponent<Animator>();
        spawnManager = GameObject.Find("GameManager").GetComponent<AB_MapSpawnManager>();

        EnemiesAlive++; // 현재 있는 모든 적들의 각각의 script에서 1씩 증가
    }
     
	private void OnCollisionEnter2D(Collision2D other)
    {

        // 충돌 강도 측정 
        //Debug.Log(other.relativeVelocity.magnitude); 

        if(other.relativeVelocity.magnitude > health)
        {            
            Die();            
        }
	}

    public void Die()
    {        
        Instantiate(EffectDie, transform.position, Quaternion.identity);
        if(maxCnt < 1)
        {
            maxCnt++;
            spawnManager.enemiesDead++;
        }
        
        Destroy(gameObject);

        EnemiesAlive--;
        GameObject.Find("GameManager").GetComponent<Angry_ManagerGame>().score++;

        
    }
	
}

