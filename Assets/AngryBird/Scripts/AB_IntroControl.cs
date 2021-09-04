using UnityEngine;

public class AB_IntroControl : MonoBehaviour
{
    public GameObject FirstBomb;

    // called when intro ends
    // Intro 끝난후 첫번째로 날릴 Bomb active 
    public void IntroEnds()
    {
        //Angry_ManagerGame.singleton.gameMode = 1; // game play
        FirstBomb.SetActive(true); // active FirstBomb when Intro ends.
    }
}
