using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement_NM : MonoBehaviour
{
    Transform _player;
    NavMeshAgent _nav;

    PlayerHealth_NM _playerHealth;
    EnemyHealth_NM _enemyHealth;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _nav = GetComponent<NavMeshAgent>();

        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth_NM>();
        _enemyHealth = GetComponent<EnemyHealth_NM>();
    }

    private void Update()
    {
        if (GameManager_NM.singleton.gameMode != 1)
        {
            _nav.speed = 0f;
        }
        else
        {
            _nav.speed = 3f;
        }

        if (!_playerHealth.isDead && !_enemyHealth.isDead)
            _nav.SetDestination(_player.position);
        else
            _nav.enabled = false;


    }
}
