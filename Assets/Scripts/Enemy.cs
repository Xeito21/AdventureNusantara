using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float speed;

    [Header("ToPoint")]
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    private Rigidbody2D musuh;
    private Transform currentPoint;
    [SerializeField] Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    private void Start()
    {
        musuh = GetComponent<Rigidbody2D>();
        currentPoint = endPoint.transform;

    }

    private void Update()
    {
        if(isChasing){
            if(transform.position.x > playerTransform.position.x){
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if(transform.position.x < playerTransform.position.x){
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            }
        else{
        if(Vector2.Distance(transform.position,playerTransform.position) < chaseDistance){
                isChasing = true;
            }
        if(currentPoint == endPoint.transform){
            musuh.velocity = new Vector2(speed, 0);
            }
        else{
            musuh.velocity = new Vector2(-speed, 0);
            }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == endPoint.transform){
            FlipEnemy();
            currentPoint = startPoint.transform;
            }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == startPoint.transform){
            FlipEnemy();
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
}
