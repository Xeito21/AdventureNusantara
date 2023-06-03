using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Sprites Bendera")]
    public Sprite passive, active;
    Collider2D coll;

    [Header("Respawn")]
    public Transform respawnPoint;

    [Header("References")]
    PlayerManager playerManager;
    SpriteRenderer spriteBendera;


    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        spriteBendera = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerManager.UpdateCheckpoint(respawnPoint.position);
            spriteBendera.sprite = active;
            coll.enabled = false;
            
        }
    }
}
