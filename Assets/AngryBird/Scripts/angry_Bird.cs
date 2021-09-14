using UnityEngine;

public class angry_Bird : MonoBehaviour {
	private Animator _anims;
	private BoxCollider2D _col;

    public float health = 4f;
    public GameObject EffectDie;

    
    private AB_CreateMap createMap;
    private int maxCnt = 0;

    void Start()
    {
		_anims = GetComponent<Animator>();
        createMap = GameObject.Find("GameManager").GetComponent<AB_CreateMap>();        
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
            createMap.enemiesDead++;            
        }
        
        Destroy(gameObject);
        
    }
	
}

