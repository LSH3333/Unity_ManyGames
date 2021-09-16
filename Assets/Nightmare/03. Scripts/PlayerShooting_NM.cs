using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting_NM : MonoBehaviour
{
    public float timeBetweenBullets = 0.15f; // 플레이어 공격간의 범위
    public float range = 100f; // 공격 범위

    float timer; // 플레이어 공격간의 간격을 만들기 위해
    Ray shootRay = new Ray();
    RaycastHit shootHit; // 빔을 쏘았을때 맞은 물체가 있을 시의 물체 정보를 담음
    int shootableMask; // 빔을 맞는 물체에 대한 일종의 필터
    ParticleSystem gunParticles; // 총이 발사될 떄 총구에서 퍼지는 효과 처리
    LineRenderer gunLine; // 보이지 않는 빔을 시각적으로 보이도록 처리
    AudioSource gunAudio; // 총 발사시의 효과음 재생
    Light gunLight; // 총이 발사될때 총구에서 밝아지는 효과 처리
    float effectsDisplayTime = 0.2f; // 총 1발이 발사될 때의 효과가 유지되는 시간

    private int damagePerShoot = 20; // enemy에게 20씩 데미지 입히도록 함 

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        // 컴포넌트 참조 변수를 모두 연결 처리한다
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

    }

    private void Update()
    {
        if (GameManager_NM.singleton.gameMode != 1) return;

        timer += Time.deltaTime;

        // 마우스 좌클릭 && 공격이 가능한 시간 간격이 되었나 
        if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
            DisableEffects(); // 총 발사에 따라 처리된 효과를 없앤다.
    }

    public void DisableEffects() // 총 발사에 따라 처리된 효과 비활성 
    {
        gunLine.enabled = false; // 총발사 시각적 효과 없엠
        gunLight.enabled = false; // 총구 앞 라이트 효과 없엠 
    }

    void Shoot() // 총 발사 
    {
        timer = 0f; // 공격 및 효과 처리를 위한 타이머의 초기화

        gunAudio.Play(); // 사운드 효과 재생
        gunLight.enabled = true; // 총구 앞 라이트 효과

        gunParticles.Stop(); // 총구 앞 화염 효과
        gunParticles.Play();

        gunLine.enabled = true; // 총 발사 시각적 효과 활성
        gunLine.SetPosition(0, transform.position); // 시각적 선의 출발점 지정.

        shootRay.origin = transform.position; // beam이 시작되는 지점 설정
        shootRay.direction = transform.forward; // beam이 발사되는 방향 설정 (z축 증가 방향)

        // beam을 (shootRay)
        // range까지만 (range) 발사하여
        // 그 beam을 맞은 물체 중에서 shootableMask로 필터링된 물체(즉, enemy)에 맞았다면
        // 그 물체에 대한 정보를 shootHit에 기록한다
        // 이를 바탕으로 beam 발사 (Raycast)
        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth_NM enemyHealth = shootHit.collider.GetComponent<EnemyHealth_NM>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damagePerShoot, shootHit.point);

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // 발사 결과가 '거짓'이어서 맞은 물체가 없다면
            // 그냥 beam의 끝지점까지 시각적 효과 처리
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    } // end of Shoot 
}
