using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    private int DestroyedPlatformsLimit = 30;

    private int NumDestroyedPlatforms = 0; // 제거된 발판의 갯수

    public int _NumDestroyedPlatforms
    {
        get { return NumDestroyedPlatforms; }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(NumDestroyedPlatforms >= DestroyedPlatformsLimit) // 제거된 발판이 70개가 넘어가면
        {
            // 새로 발판 생성
            GameObject.Find("GameManager").GetComponent<LevelGenerator>().GeneratePlatforms();
            NumDestroyedPlatforms = 0; // 제거된 발판 갯수 초기화
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Fireball") { // Fireball은 Destroyer에 의해 제거되지 않아야함
        NumDestroyedPlatforms++; // 제거될때마다 숫자 카운트
        Destroy(collision.gameObject); // Destroyer에 닿은 오브젝트들 제거
        }
    }
}
