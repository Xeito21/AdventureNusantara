using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] int damage;

    [Header("Range")]
    [SerializeField] float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;

    [Header("Cooldown Attack")]
    private float cooldownTimer = Mathf.Infinity;

    [Header("Layer Target")]
    [SerializeField] LayerMask playerLayer;

    [Header("References")]
    private PlayerManager playerManager;
    [SerializeField] Enemy enemySkrip;
    [SerializeField]bool isPlayerinArea=false;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

       // if (PlayerInsight())
       if(isPlayerinArea)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                MeleeAttack();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){ 
         if(other.gameObject.tag =="Player"){
            
        print("player="+other.gameObject.name);
           isPlayerinArea=true;
           playerManager=other.gameObject.GetComponent<PlayerManager>();
        }
    }
     void OnTriggerExit2D(Collider2D other){
         if(other.gameObject.tag =="Player"){
           isPlayerinArea=false;
            playerManager=null;
        }
    }
   /* 
   private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                              new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                                              0,
                                              Vector2.left,
                                              0,
                                              playerLayer);

        if (hit.collider != null)
        {
            playerManager = hit.transform.GetComponent<PlayerManager>();
        }

        return hit.collider != null;
    }
    */

    private void MeleeAttack()
    {
        DamagePlayer();
        //animasi enemyattack
    }

    private void DamagePlayer()
    {
        if (playerManager != null)
      {
            playerManager.TerkenaDamage(enemySkrip);
        }
    }
/*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center  * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }*/
}
