using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Sprites Bendera")]
    public Sprite passive, active;
    [SerializeField] private Collider2D coll;

    [Header("Respawn")]
    public Transform respawnPoint;

    [Header("References")]
    public PlayerManager playerManager;
    SpriteRenderer spriteBendera;


    private void Awake()
    {
        spriteBendera = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.Play("CheckPoint");
            playerManager.UpdateCheckpoint(respawnPoint.position);
            spriteBendera.sprite = active;
            coll.enabled = false;
            
        }
    }
}
