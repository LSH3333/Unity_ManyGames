using UnityEngine;
// 이 코드는 collider에 접촉하면 지우고, 그 순간 위쪽에 추가로 발판들 생성하는 코드.
// 이제는 안씀.
public class Destroyer_Disposal : MonoBehaviour
{
    public GameObject player;
    public GameObject FloorPrefab;
    public GameObject Plat_black, Plat_red, Plat_blue, Plat_yellow;

    private int PlatColorIndex;

    // 밑의 Platform이 사라질때마다 새로운 Platform을 위에 생성
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlatColorIndex = Random.Range(0, 4); // black, red, blue, yellow
        
        switch(PlatColorIndex)
        {
            case 0:
                Instantiate(FloorPrefab,
            new Vector2(Random.Range(-2.2f, 2.2f), player.transform.position.y + (5f + Random.Range(0.2f, 0.8f))),
            Quaternion.identity);
                break;

            case 1:
                Instantiate(Plat_red,
            new Vector2(Random.Range(-2.2f, 2.2f), player.transform.position.y + (5f + Random.Range(0.2f, 0.8f))),
            Quaternion.identity);
                break;

            case 2:
                Instantiate(Plat_blue,
            new Vector2(Random.Range(-2.2f, 2.2f), player.transform.position.y + (5f + Random.Range(0.2f, 0.8f))),
            Quaternion.identity);
                break;

            case 3:
                Instantiate(Plat_yellow,
            new Vector2(Random.Range(-2.2f, 2.2f), player.transform.position.y + (5f + Random.Range(0.2f, 0.8f))),
            Quaternion.identity);
                break;

            case 4:
                Instantiate(FloorPrefab,
            new Vector2(Random.Range(-2.2f, 2.2f), player.transform.position.y + (5f + Random.Range(0.2f, 0.8f))),
            Quaternion.identity);
                break;
        }
        Destroy(collision.gameObject);
    }

    private void RandomSpawn()
    {
        int PlatformNum = Random.Range(0, 3);
        float RangeY = player.transform.position.y + (5f + Random.Range(0.2f, 0.8f));        

        for(int i = 0; i < PlatformNum; i++)
        {
            Instantiate(FloorPrefab,
                new Vector2(Random.Range(-2.2f, 2.2f), RangeY), Quaternion.identity);
        }
    }
}
