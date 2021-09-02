using UnityEngine;

public class AB_DieEffectDestroy : MonoBehaviour
{
    // Animation CollisionBomb에서 effect 끝날때 호출 
    void DestroyDieEffect()
    {
        Destroy(gameObject);
    }
}
