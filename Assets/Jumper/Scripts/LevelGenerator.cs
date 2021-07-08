using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int numberOfPlatforms = 100;
    public float levelWidth = 2.2f;
    public float minY = .4f;
    public float maxY = .8f;

    public GameObject Plat_black, Plat_red, Plat_blue;
    private int PlatColorIndex;

    Vector3 spawnPosition = new Vector3();

    private void Start()
    {  
        GeneratePlatforms(); // 시작할때 발판 생성
    }

    // 발판 생성. 
    public void GeneratePlatforms()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            PlatColorIndex = Random.Range(0, 4); // black, black, red, blue // black이 더큰확률

            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            switch (PlatColorIndex)
            {
                case 0: // black
                    Instantiate(Plat_black, spawnPosition, Quaternion.identity);
                    break;

                case 1: // black
                    Instantiate(Plat_black, spawnPosition, Quaternion.identity);
                    break;

                case 2: // red
                    Instantiate(Plat_red, spawnPosition, Quaternion.identity);
                    break;

                case 3: // blue
                    Instantiate(Plat_blue, spawnPosition, Quaternion.identity);
                    break;

                  
            }
        }
    }

    private void GenerateItem()
    {
        
    }
}
