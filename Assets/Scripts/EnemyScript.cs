using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] float speed;
    Rigidbody2D enemyRb;



    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        enemyRb.velocity = new Vector2(speed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speed = -speed;
        FlipEnemy();
    }
    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRb.velocity.x)), 1f);
    }
}
