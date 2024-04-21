using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Fireball2Prefab;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Shoot();
            nextAttackTime = Time.time + 1f / attackRate;

        }
    }
    void Shoot()
    {
        Instantiate(Fireball2Prefab, FirePoint.position, FirePoint.rotation);
    }
}
