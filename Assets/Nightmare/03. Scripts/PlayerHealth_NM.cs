using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth_NM : MonoBehaviour
{
    public int startingHealth = 100; // 시작 hp 값
    private int currentHealth; // 플레이어 현재 hp 값
    private Slider healthSlider; // 슬라이더 게이지 참조
    private Image damageImage; // 피격 효과를 위한 이미지 참조
    private AudioSource pAudio; // 피격 사운드 효과를 위한 AudioSource 참조 
    private float flashSpeed = 5f; // 번쩍 효과를 위한 진행 속도
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f); // 번쩍일 때의 컬러값

    private bool damaged = false; // 번쩍 효과 처리를 위한 플래그

    private Animator anim; // 플레이어의 죽는 애니메이션 전환을 처리
    private PlayerMovement_NM playMovement; // 죽은 후 움직이지 않게 처리 함
    private PlayerShooting_NM playShooting; // 죽은 후 총 발사 않게 함.
   
    public bool isDead = false; // 죽음 여부에 대한 플래그
    public AudioClip deadClip; // 죽을 때의 음량 효과 클립

    // 죽은 후 1인칭에서도 움직이지 않도록 
    private PlayerMovementFristPersonView_NM playerMovement1st;

    private void Awake()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        damageImage = GameObject.Find("DamageEffect").GetComponent<Image>();
        pAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playMovement = GetComponent<PlayerMovement_NM>();
        playShooting = GetComponentInChildren<PlayerShooting_NM>(); // 자식 오브젝트까지 탐색
        playerMovement1st = GetComponent<PlayerMovementFristPersonView_NM>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        // 슬라이더 게이지의 최대값, 현재값을 설정 
        healthSlider.maxValue = startingHealth;        
        healthSlider.value = startingHealth;
    }

    private void Update()
    {
        //print("hp = " + currentHealth);
        if (GameManager_NM.singleton.gameMode != 1) return;

        if (damaged)
            damageImage.color = flashColor;
        else
        {
            // 새로운 '번쩍' 컬러값이 설정된 후 서서히 알파값을 '0'으로 변경시키고 있다
            damageImage.color =
                Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        print("TakeDamage");
        damaged = true; // '번쩍' 효과를 위해
        currentHealth -= amount; // 현재 HP값을 감소시킨다
        healthSlider.value = currentHealth; // 슬라이더 UI에 반영
        pAudio.Play(); // 피격 사운드 재생
        if (currentHealth <= 0 && !isDead) Death(); // HP 없고, 죽지 않았다면 '죽인다' 
    }

    void Death()
    {
        // 죽음 flag 설정
        isDead = true;
        // 총을 발사하고 있었고, 효과가 아직 남아 있을 수 있으니 효과를 없엔다
        playShooting.DisableEffects();
        // 애니메이션 전환
        anim.SetTrigger("Die");
        // '죽음' 사운드 재생
        pAudio.clip = deadClip;
        pAudio.Play();
        // 움직이지 못하게, 총 발사하지 못하게 스크립트 비활성화 처리
        playMovement.enabled = false;
        playShooting.enabled = false;
        playerMovement1st.enabled = false;

        // Player 죽으면 게임오버, 결과창 띄움 
        GameManager_NM.singleton.SetGameOver();
    }
}
