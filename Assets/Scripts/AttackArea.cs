using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public QuestionManager questionManager; 
    public ButoEnemy butoEnemy;

    private void Awake()
    {
        ButoEnemy butoEnemy = GetComponent<ButoEnemy>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            butoEnemy.KnockBackButo();
            float damage = 1;
            butoEnemy.KenaDamage(damage);
        }
    }
}