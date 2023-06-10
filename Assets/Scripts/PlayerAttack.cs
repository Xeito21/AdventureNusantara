using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float timeAttack = 1f;

    [Header("Target")]
    private GameObject attackArea = default;

    [Header("Cooldown")]
    private float timer = 0f;

    [Header("Boolean")]
    private bool attacking = false;


    [Header("References")]
    private PlayerMovement playerMovement;
    public ButoEnemy butoEnemy;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && PlayerManager.Instance.animPlayer.GetBool("isGrounded"))
        {
            if(!attacking )
            {
                Attack();
            }
        }
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }
    private void Attack()
    {
        AudioManager.Instance.Play("Punch");
        attacking = true;
        attackArea.SetActive(attacking);
        PlayerManager.Instance.animPlayer.SetTrigger("isAttack");
    }

}