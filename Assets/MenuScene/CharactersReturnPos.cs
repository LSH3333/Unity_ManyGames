using UnityEngine;

public class CharactersReturnPos : MonoBehaviour
{
    public GameObject returnpos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 캐릭터들이 화면 좌측끝에 도달하면 오른쪽 끝(returnpos)에 돌아오도록함 
        collision.gameObject.transform.position = returnpos.transform.position;
    }

    
}

