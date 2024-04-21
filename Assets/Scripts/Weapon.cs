using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Fireball1Prefab;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            nextAttackTime = Time.time + 1f / attackRate;
        }

        }
        
    }
    void Shoot ()
    {
        Instantiate(Fireball1Prefab, FirePoint.position, FirePoint.rotation);
    }
}
