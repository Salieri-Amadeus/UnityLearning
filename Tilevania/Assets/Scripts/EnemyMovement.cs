using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        myRigidBody2D.velocity = new Vector2(moveSpeed, myRigidBody2D.velocity.y);
        
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Ground"){
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }
        
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }
}
