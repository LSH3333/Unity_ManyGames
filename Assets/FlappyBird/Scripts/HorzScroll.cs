using UnityEngine;
using HorzTools;

[RequireComponent(typeof(Rigidbody2D))]
public class HorzScroll : HScroll
{
    private void Start()
    {
        // Rigidbody Kinematic으로 무력화
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (FlappyBird_ManagerGame.inst.isGameOver || FlappyBird_ManagerGame.inst.gameMode == 3)
        {
            setStop();
        }
        else if(FlappyBird_ManagerGame.inst.gameMode == 1)
        {
            GameStart();
        }
    }

    void GameStart()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // RIgidbody Dynamic으로 바꾸고
        setRigidbody(2f);
    }
}
