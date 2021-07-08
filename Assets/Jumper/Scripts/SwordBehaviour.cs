using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    public BoxCollider2D _col;
    public Animator _ani; // Player Animator

    private Rigidbody2D _r2bd; // Player's Rigidbody

    public int NumberOfFireballDestroyed = 0;
    private GameObject gaugeScript;

    private void Start()
    {
        _r2bd = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        gaugeScript = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Fireball") { // Fireball과 접촉했을때
            // 현재 Player가 Attack 중 이라면
            if (_ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                // Fireball을 파괴
                col.gameObject.GetComponent<Firball>().FireballDestroy();
                AddForceToPlayer();

                //Debug.Log("fireball: " + NumberOfFireballDestroyed);
                NumberOfFireballDestroyed++; // Destroyed Fireball number increase
                // gauge Script update
                gaugeScript.GetComponent<gauge>().GaugeUpdate(NumberOfFireballDestroyed);

                if(NumberOfFireballDestroyed >= 5) // Destroyed Fireball의 수가 일정수 넘으면 booster_trigger
                {
                    GameObject.Find("Player").GetComponent<Booster>().booster_trigger = true;
                    NumberOfFireballDestroyed = 0; // booster 작동 후 number 초기화
                    gaugeScript.GetComponent<gauge>().ResetGauge(); // reset gauge
                }

               
            }
        }
    }

    private void AddForceToPlayer()
    {
        // Player가 낙하중일때
        if(_r2bd.velocity.y <= 0)
        {
            _r2bd.AddForce(new Vector2(0, 400f));
        }
        else // 아닐때
        {
            _r2bd.AddForce(new Vector2(0f, 200f));
        }
    }
}
