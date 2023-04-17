using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rigidBody2D;
    Animator animator;
    CapsuleCollider2D playerCollider2D;
    BoxCollider2D feetCollider2D;
    SpriteRenderer spriteRenderer;
    bool playerHasHorizontalSpeed = false;
    bool playerHasVerticalSpeed = false;
    bool isAlive = true;

    float startGravityScale;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startGravityScale = rigidBody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){
            return;
        }
        Run();
        Climb();
        Die();
        FlipSprite();
        InputDetect();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveSpeed * moveInput.x, rigidBody2D.velocity.y);
        rigidBody2D.velocity = playerVelocity;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void Climb(){
        if(!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            animator.SetBool("isClimbing", false);
            rigidBody2D.gravityScale = startGravityScale;
            return;
        }
        Vector2 climbVelocity = new Vector2(rigidBody2D.velocity.x, climbSpeed * moveInput.y);
        rigidBody2D.velocity = climbVelocity;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
        rigidBody2D.gravityScale = 0f;
    }

    void OnMove(InputValue value){
        if(!isAlive){
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value){
        if(!isAlive){
            return;
        }
        if(!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        if(value.isPressed){
            rigidBody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void FlipSprite(){
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(rigidBody2D.velocity.x), 1f);
        }
    }

    void InputDetect(){
        playerHasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        playerHasVerticalSpeed = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
    }

    void Die(){
        if(playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies"))){
            isAlive = false;
            animator.SetTrigger("Dying");
            rigidBody2D.velocity = new Vector2(0f, 10f);
            spriteRenderer.color = new Color(1f, 0f, 0f, spriteRenderer.color.a);
        }
    }

}

