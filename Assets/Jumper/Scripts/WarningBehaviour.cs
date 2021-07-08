using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningBehaviour : MonoBehaviour
{

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, Camera.main.transform.position.y+4.5f);    
    }


    private void DestroyThis()
    {

        Destroy(gameObject);
    }
}
