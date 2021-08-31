﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    private bool clickedOnBomb = false;

    private Ray _rayToCatapult; // 최대 제한 거리 지정을 위한 레이저빔
    private float _maxLengh = 3f; // 최대 제한 거리
    public Transform _zeroPoint; // 원점. 유니티 에디터에서 지정함.

    private Rigidbody2D _rb2d;
    private SpringJoint2D _spring;
    private bool _springDestroy = false;
    private bool ColliderTrigger = false; // OnCollisionEnter 한번만 호출하도록

    private Vector2 _prev_velocity; // 매 프레임 rigidbody의 바로 전 속도.

    public GameObject nextBomb; // 다음으로 날릴 Bomb
    public TrailRenderer _trail; // Child의 Trail renderer

    // SpawnMap script     
    public AB_MapSpawnManager spawnWood;
    // Bomb Prefab 
    public GameObject preBomb;

    public bool SpringDestroy
    {
        get { return _springDestroy;  }
    }

    void Start()
    {        
        // _zeroPoint에서 Vector3.zero(0,0,0)으로의 Ray 생성
        _rayToCatapult = new Ray(_zeroPoint.position, Vector3.zero);

        _rb2d = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
        _springDestroy = false;
    }

    private void Update()
    {
        if(clickedOnBomb)
        {
            Vector3 mouseWorldPoint =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mouseWorldPoint에서 zeroPoint를 뺌으로서 벡터의 길이가됨.
            Vector2 _newVector = mouseWorldPoint - _zeroPoint.position; // 새 벡터 지정.
            // sqrMagnitude는 벡터의 제곱값 구함, 따라서 비교할때도 _maxLength * _maxLength와 비교
            if(_newVector.sqrMagnitude > _maxLengh * _maxLengh) // 제한거리보다 멀리 있다면
            {
                _rayToCatapult.direction = _newVector; // ray 지정하고
                mouseWorldPoint = _rayToCatapult.GetPoint(_maxLengh); // 제한거리 위치 얻음.
            }


            mouseWorldPoint.z = 0f;

            // 스톤의 위치를 마우스 클릭 지점으로 옮긴다 
            // spring joint component가 없을때만, 즉 쏘기전에만 
            if(_spring != null)
                transform.position = mouseWorldPoint;
        }

        if(_spring != null)
        {
            // 바로 전 속력이 크다는 것은 감속이 시작되었다고 인지할 수 있다.
            if(_prev_velocity.sqrMagnitude > _rb2d.velocity.sqrMagnitude)
            {
                Destroy(_spring); // spring 제거
                _springDestroy = true;
                _rb2d.velocity = _prev_velocity; // 마지막 속력값을 지정.
            }
            if (clickedOnBomb == false) _prev_velocity = _rb2d.velocity;
        }
        
        
    }
    /*
    void OnDisable()
    {
        
        _trail.autodestruct = true;
    }
    */
    
    // Bomb이 파괴될때 child와 parent 분리 (trail renderer 유지 위해서)
    void OnDestroy()
    {
        _trail.transform.parent = null;
    }

    void OnMouseDown() {
        clickedOnBomb = true;
    }

    void OnMouseUp()
    { 
        clickedOnBomb = false;

        // rigidbody가 자신의 일을 하도록 풀어준다
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ColliderTrigger) return; // OnCollisionEnter2D가 이미 한번 호출된 상태면 더이상 호출되지않음
        Debug.Log("Collision Enter");
        ColliderTrigger = true;
        StartCoroutine(DestroyBomb());
    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(5f);
        //gameObject.SetActive(false); // 현재 Bomb disable
        
        // 3마리의 EnemyBird를 다 맞췄고 현재 마지막 Bomb이었다면 
        // 새로운 Bomb를 소환한후 nextBomb에 연결 
        if(spawnWood.enemiesDead == 3 && nextBomb == null)
        {
            GameObject nextBombObject = Instantiate(preBomb, preBomb.transform.position, Quaternion.identity) as GameObject;
            nextBomb = nextBombObject;
        }

        if(nextBomb != null) {  // nextBomb이 있을때만
        nextBomb.SetActive(true); // 다음 Bomb active

        // 카메라도 다음 Bomb로 이동
        Camera.main.GetComponent<CameraFollowBomb>().SwitchBomb();
        }
        else // this is when game is over
        {
            // Level Restart
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Angry_ManagerGame.singleton.setGameOver();
            GameObject.Find("GameManager").GetComponent<GameOverFunction>().setGameOver();
        }

        Debug.Log("dead: " + spawnWood.enemiesDead);
        spawnWood.enemiesDead = 0;

        Destroy(gameObject); // 현재 Bomb 파괴
        Destroy(GameObject.Find("WoodStructure(Clone)")); // 생성된 WoodStructure 파괴 

        

        // 새로운 WoodStructure 생성 
        spawnWood.SpawnWoodStructure();
    }
   
    


}