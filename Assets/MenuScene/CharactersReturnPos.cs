using UnityEngine;

public class CharactersReturnPos : MonoBehaviour
{
    public GameObject birdReturnpos;
    public GameObject returnPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "MainMenuBird")
        {
            // Bird 화면 좌측끝에 도달하면 오른쪽 끝(birdReturnpos)에 돌아오도록함 
            collision.gameObject.transform.position = birdReturnpos.transform.position;
        }
        else
        {
            collision.gameObject.transform.position = returnPos.transform.position;
        }
       
    }

    
}

