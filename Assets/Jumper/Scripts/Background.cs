using UnityEngine;

public class Background : MonoBehaviour
{
    private BoxCollider2D _box;
    private float BackgroundLength; // 배경의 height

    private void Start()
    {
        BackgroundLength = GetComponent<RectTransform>().rect.height;
        
    }
    
    void RepositionSky()
    {
        Vector2 _move = new Vector2(0f, BackgroundLength * 2);
        transform.position = (Vector2)transform.position + _move;
    }

}
