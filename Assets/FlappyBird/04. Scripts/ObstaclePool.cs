using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    private GameObject _prefColumn;
    private GameObject[] _pColumns;
    private int _colPoolSize = 5;
    private int _currentColIndex = 0;
    private float _colSpawnRate = 3f;
    private float _spawnXPosition = 10f;
    private float _colYPositionMax = 3f;
    private float _colYPositionMin = -0.5f;

    private void Start()
    {
        _prefColumn = Resources.Load("Columns") as GameObject;
        //InitColumnCreate();
    }

    public void InitColumnCreate()
    {
        _pColumns = new GameObject[_colPoolSize];
        for(int i = 0; i < _pColumns.Length; i++)
        {
            _pColumns[i] = Instantiate(
                _prefColumn, 
                new Vector2(-15f, -25f), 
                Quaternion.identity);

            
        }
        InvokeRepeating("Spawn", 0f, _colSpawnRate);
    }

    void Spawn()
    {
        if (FlappyBird_ManagerGame.inst.isGameOver) return;

        float _y_position = Random.Range(_colYPositionMin, _colYPositionMax);
        _pColumns[_currentColIndex].transform.position =
            new Vector2(_spawnXPosition, _y_position);

        _currentColIndex = (_currentColIndex + 1) % _colPoolSize;
    }
}
