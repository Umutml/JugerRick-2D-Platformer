using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D playerRb;
    Animator playerAnim;
    CapsuleCollider2D playerCapsuleColl;
    BoxCollider2D playerBoxColl;

    [SerializeField] GameObject rocketsParticle;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float rocketForce;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject barrelGo;
    private bool isFlying;
    private bool isAlive = true;
    [SerializeField]private Vector2 deathForce = new Vector2(0,10);
    
    void Start()
    {
        playerCapsuleColl = GetComponent<CapsuleCollider2D>();
        playerAnim = GetComponent<Animator>();
        playerBoxColl = GetComponent<BoxCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isAlive) {
            rocketsParticle.SetActive(false);
            return; }
        Walk();
        RocketOn();
        Die();
        Shoot();
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) return;
            if (!playerBoxColl.IsTouchingLayers(LayerMask.GetMask("Platforms")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRb.velocity += new Vector2(0f, jumpForce);
            playerAnim.SetTrigger("Jump");
        }
    }
    private void Walk()
    {
        
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRb.velocity.y);
        playerRb.velocity = playerVelocity;
        FlipPlayer();
        if (moveInput.x != 0 && !isFlying) 
        { 
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
    }    

    void FlipPlayer()
    {
        if (playerRb.velocity.x < 0)
            transform.localScale = new Vector2(-1, 1);              // Player turn facing left and right looking velocity.x
        else if (playerRb.velocity.x > 0)
            transform.localScale = new Vector2(1, 1);
    }

    void RocketOn()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerRb.AddRelativeForce(Vector2.up * rocketForce * Time.deltaTime);
            rocketsParticle.SetActive(true);
            isFlying = true;
        }
        else 
        { 
        rocketsParticle.SetActive(false);
        isFlying = false;
        }
    }
    void Die()
    {
        if (playerCapsuleColl.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            playerAnim.SetTrigger("isDead");
            playerRb.velocity = deathForce;
        }
        if (playerCapsuleColl.IsTouchingLayers(LayerMask.GetMask("Spike")))
        {
            isAlive = false;
            playerAnim.SetTrigger("isDead");
            playerRb.velocity = deathForce;
        }
        if (playerCapsuleColl.IsTouchingLayers(LayerMask.GetMask("Acid")))
        {
            isAlive = false;
            playerAnim.SetTrigger("isDead");
            //playerRb.velocity = deathForce;
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.SetTrigger("Shoot");
            GameObject bulletTemp = Instantiate(bulletPrefab, barrelGo.transform.position, barrelGo.transform.rotation);
            Destroy(bulletTemp, 1.5f);
        }
    }
}


