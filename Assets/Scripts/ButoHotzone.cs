using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButoHotzone : MonoBehaviour
{
    private ButoEnemy butoEnemy;
    private bool inRange;
    private Animator butoAnim;

    private void Awake()
    {
        butoEnemy = GetComponentInParent<ButoEnemy>();
        butoAnim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(inRange && !butoAnim.GetCurrentAnimatorStateInfo(0).IsName("Buto_Attack"))
        {
            butoEnemy.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            butoEnemy.triggerArea.SetActive(true);
            butoEnemy.inRange = false;
            butoEnemy.SelectTarget();
        }
    }
}
