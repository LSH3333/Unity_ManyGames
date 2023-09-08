using UnityEngine;

public class EnemyAttack_NM : MonoBehaviour
{
    private const float timeBetweenAttacks = 0.5f; // 무한 공격을 방지하기 위해 시간 간격을 둠
    private const int attackDamage = 2; // 한 번의 공격으로 입히는 데미지 값

    private GameObject player; // 플레이어 오브젝트
    private PlayerHealth_NM playerHP;
    private bool playerInRange = false; // 플레이어가 공격 범위 내에 있는지 여부
    private float timer; // 무한 공격 방지 타이머

    private Animator anim;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerHP = player.GetComponent<PlayerHealth_NM>();
        anim = GetComponent<Animator>();
    }

    // 'Zombunny'의 sphere collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = true;
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = false; // 공격 범위에서 벗어났다 
    }

    private void Update()
    {
        //Debug.Log("Timer: " + timer);
        // Player가 공격범위에있고, 공격 가능 타이머라면 공격
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
            Attack();

        if (playerHP.isDead) anim.SetTrigger("PlayerDead");
    }

    void Attack()
    {
        if (GameManager_NM.singleton.gameMode != 1) return;

        timer = 0f;
        if(!playerHP.isDead)
            playerHP.TakeDamage(attackDamage);
    }

    
}
