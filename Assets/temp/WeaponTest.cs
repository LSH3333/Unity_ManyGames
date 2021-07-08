using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{

    public GameObject yourWeapon;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(yourWeapon.GetComponent<Weapon>().GetType().Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
