using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    bool hasBeenPickedUp = false;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && !hasBeenPickedUp){
            hasBeenPickedUp = true;
            FindObjectOfType<GameSession>().earnCoin();
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
