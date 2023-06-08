using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buto_HitBox : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.TerkenaDamage();
            playerMovement.knockBackCounter = playerMovement.knockBackTotalTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.knockFromRight = false;
            }
        }
    }
}
