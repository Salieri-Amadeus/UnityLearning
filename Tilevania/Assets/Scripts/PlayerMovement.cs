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
    bool playerHasHorizontalSpeed = false;
    bool playerHasVerticalSpeed = false;

    float startGravityScale;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        startGravityScale = rigidBody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Climb();
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
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value){
        if(!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){
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

}

