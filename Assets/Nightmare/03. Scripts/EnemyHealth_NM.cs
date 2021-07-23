using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth_NM : MonoBehaviour
{
    private int startingHealth = 100; // enemy 시작 hp
    private int currentHealth;  // enemy 현재 hp
    private float sinkSpeed = 2.5f; // enemy 죽을때 땅으로 꺼지는 속도
    public AudioClip deathClip; // enemy 죽을때 효과음

    private Animator anim; // enemy 애니메이션 전환용
    private AudioSource enemyAudio; // 캐릭터 효과음 재생
    private ParticleSystem hitParticles; // 피격시 피격 지점에서 먼지가 날리는 효과
    private CapsuleCollider capsuleCollider; // enemy 물리처리를 위한 Collider
    public bool isDead; // enemy가 죽었는지
    private bool isSinking; // enemy 가라앉고 있는 중인지

    private GameManager_NM gm;

    private void Awake()
    {
        // GameManager_NM 연결 
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager_NM>();
        
        // 참조 변수들 연결 처리
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        // 초기값 설정
        currentHealth = startingHealth;

    } // end of Awake()

    private void Update()
    {
        if(isSinking) // enemy가 땅으로 가라앉는 중이면 
        {
            // transform.Translate : Translate(float x, float y, float z)
            // Moves the transform in the direction and distance of translation.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime); // 아래로 이동 
        }
    }

    // enemy가 데미지 받음. 즉 currentHealth 감소 시킴
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return; // 죽은 상태에서는 데미지가 없다

        enemyAudio.Play(); // 피격 효과음 재생
        currentHealth -= amount; // HP값 amount만큼 감소시킴
        hitParticles.transform.position = hitPoint; // 피격지점을 바탕으로 효과 재생 위치 지정
        hitParticles.Play();

        if(currentHealth <= 0) // hp가 0이면
        {
            Death(); // 죽인다 
        }

    }

    void Death()
    {
        isDead = true;

        // monster 죽으면 점수 상승 
        GameManager_NM.singleton.score++;

        capsuleCollider.isTrigger = true; // 가라앉게 하기위해 물리효과 없엠
        anim.SetTrigger("Dead"); // 죽는 애니메이션으로 전환
        enemyAudio.clip = deathClip; // 효과음 음원 교체
        enemyAudio.Play(); // 효과음 재생 
    }

    // clip에서 animation event로 구성된 함수
    // 접근제어 public 
    public void StartSinking()
    {
        // 네비게이션 기능을 없에서 쫒아오지 못하도록 함
        GetComponent<NavMeshAgent>().enabled = false;
        // Rigidbody 무력화
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        // 2초 뒤에 사라지도록 처리
        Destroy(gameObject, 2f);
    }
    
} // end of EnemyHealth.cs 
