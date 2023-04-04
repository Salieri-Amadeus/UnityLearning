using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    void Start()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if(quiz.isCompleted){
            endScreen.gameObject.SetActive(true);
            quiz.gameObject.SetActive(false);
            endScreen.displayScore();
        }
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
