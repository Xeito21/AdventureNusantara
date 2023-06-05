using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;
    [SerializeField] int damage;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Update () {
        cooldownTimer += Time.deltaTime;

        if(playerInsight()){
            if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
    }

    private bool playerInsight(){
        RaycastHit2D hit = 
        Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                          new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                          0,
                          Vector2.left,
                          0,
                          playerLayer);
            if( hit.collider != null){
                playerHealth = hit.transform.GetComponent<Health>();
            }
        return hit.collider != null;
    }
    private void OnDrawGizmos() {
        Gizmos.color =Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer () {
        if(playerInsight()){
            playerHealth.TakeDamage(damage);
        }
    }
}
