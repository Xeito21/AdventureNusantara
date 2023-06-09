using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButoCheck : MonoBehaviour
{
    private ButoEnemy butoEnemy;

    private void Awake()
    {
        butoEnemy = GetComponentInParent<ButoEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            butoEnemy.target = collider.transform;
            butoEnemy.inRange = true;
            butoEnemy.hotZone.SetActive(true);
        }
    }
}
