using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidBody2D;
    PlayerMovement player;
    [SerializeField] float bulletSpeed = 10f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        float xSpeed = player.transform.localScale.x * bulletSpeed;
        rigidBody2D.velocity = new Vector2(xSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
