using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float speed;

    [Header("ToPoint")]
    public Transform[] patrolPoint;
    public int patrolDestination;

    [Header("Target")]
    public Transform playerTransform;

    [Header("Check")]
    public bool isChasing;
    public float chaseDistance;



    private void Update()
    {
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoint[0].position) < 0.2f)
                {

                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoint[1].position) < 0.2f)
                {

                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
    }

    // private void FlipEnemy()
    // {
    //     Vector3 localScale = transform.localScale;
    //     localScale.x *= -1;
    //     transform.localScale = localScale;
    // }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(startPoint.transform.position, 0.5f);
    //     Gizmos.DrawWireSphere(endPoint.transform.position, 0.5f);
    //     Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
    // }
}