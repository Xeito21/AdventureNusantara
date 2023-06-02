using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemyHealth>() != null){
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            enemyHealth.Damage(damage);
        }        
    }
}
