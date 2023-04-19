using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    // [SerializeField] int playerlives = 3;
    int playerDeathCount = 0;
    int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI deathText;
    
    void Awake()
    {
        if(FindObjectsOfType<GameSession>().Length > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = "Coins: " + score.ToString();
        deathText.text = "Death: " + playerDeathCount.ToString();
    }

    public void ProcessPlayerDeath(){
        // if(playerlives > 0){
        //     TakeLife();
        // }
        // else{
        //     ResetGameSession();
        // }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        playerDeathCount++;
        deathText.text = "Death: " + playerDeathCount.ToString();
    }

//    void TakeLife()
//     {
//         playerlives--;
//         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
//         SceneManager.LoadScene(currentSceneIndex);
//     }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().resetPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void earnCoin(){
        score++;
        scoreText.text = "Coins: " + score.ToString();
    }

    public int calculeScore(){
        return score * 100 - playerDeathCount * 100;; 
    }
}
