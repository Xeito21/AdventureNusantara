using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float speed;
    [SerializeField] private float chaseDistance; // Jarak pengejaran musuh

    [Header("ToPoint")]
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    [Header("Enemy")]
    private Rigidbody2D musuh;
    private Transform currentPoint;
    private Transform playerTransform; // Transform pemain

    [Header("References")]
    public PlayerManager playerManager;

    private bool isChasing = false;

    private void Start()
    {
        musuh = GetComponent<Rigidbody2D>();
        currentPoint = endPoint.transform;
        playerTransform = playerManager.transform;
    }

    private void Update()
    {
        // Menghitung jarak antara musuh dan pemain
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Jika jarak kurang dari jarak pengejaran, maka musuh akan mengejar pemain
        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        // Mengubah arah pergerakan musuh menuju pemain
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        musuh.velocity = new Vector2(direction.x * speed, 0);
    }

    private void Patrol()
    {
        // Pergerakan patroli musuh antara startPoint dan endPoint
        Vector2 point = currentPoint.position - transform.position;

        if (!isChasing)
        {
            if (currentPoint == endPoint.transform)
            {
                musuh.velocity = new Vector2(speed, 0);
            }
            else
            {
                musuh.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                FlipEnemy();
                if (currentPoint == endPoint.transform)
                    currentPoint = startPoint.transform;
                else
                    currentPoint = endPoint.transform;
            }
        }
    }

    private void FlipEnemy()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(endPoint.transform.position, 0.5f);
        Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerManager.TerkenaDamage();
        }
    }
}
