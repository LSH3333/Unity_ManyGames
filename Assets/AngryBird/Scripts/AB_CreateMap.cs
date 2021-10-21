using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_CreateMap : MonoBehaviour
{
    public Transform _parent;

    private GameObject _plank, _bird;
    private float _wid = 1.6f;

    public int enemiesDead = 0;

    private void Awake()
    {
        _plank = (GameObject)Resources.Load("column_ab");
        _bird = (GameObject)Resources.Load("EnemyBird");
    }

    private void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        int maxcol = 8;
        for (int r = 0; r <= 2; r++)
        {
            maxcol = Random.Range(1, 1 + maxcol);
            CreateRows(r, maxcol);
        }
    }

    void CreateRows(int row, int col)
    {
        float s = _wid * (-col / 2) - (_wid / 2) * (col % 2);
        for (int i = 0; i < col + 1; i++) CreatePlank(s, row, i, true);
        for (int i = 0; i < col; i++) CreatePlank(s + _wid / 2, row, i, false);

        // bird 
        GameObject o = Instantiate(_bird, transform.position, Quaternion.identity);
        o.transform.SetParent(_parent);
        float x = s + _wid / 2 + Random.Range(0, col) * _wid;
        float y = -0.5f + 2f * row;
        o.transform.localPosition = new Vector2(x, y);
    }

    void CreatePlank(float s, int r, int c, bool v)
    {
        GameObject o = Instantiate(_plank, transform.position, Quaternion.identity);
        o.transform.SetParent(_parent);
        // 세로로 새워지는 plank 
        if(v)
        {
            o.transform.localRotation = Quaternion.Euler(0, 0, 90);
            o.transform.localPosition = new Vector2(s + c * _wid, r * 2);
        }
        else // 가로로 눕는 plank 
        {
            o.transform.localPosition = new Vector2(s + c * _wid, r * 2 + 1);
        }
    }
}
