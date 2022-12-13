using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRb;
    private float bulletSpeed = 5;
    private Transform rickTransform;

    void Start()
    {
        rickTransform = GameObject.Find("RickPlayer").transform;
        bulletRb = GetComponent<Rigidbody2D>();
        transform.localScale = rickTransform.localScale;
        transform.localScale = new Vector3(rickTransform.localScale.x, 1, 1);
        bulletRb.velocity = new Vector2(transform.localScale.x * bulletSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            //HitDamage
        }
        Destroy(gameObject);
    }

}


