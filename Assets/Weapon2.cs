using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : MonoBehaviour
{
    public Transform FirePoint2;
    public GameObject Fireball2Prefab;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButton("Fire5"))
            {
                Shoot();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }

    }
    void Shoot()
    {
        Instantiate(Fireball2Prefab, FirePoint2.position, FirePoint2.rotation);
    }
}
