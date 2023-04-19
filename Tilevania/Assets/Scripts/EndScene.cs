using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        int finalScore = FindObjectOfType<GameSession>().calculeScore();
        scoreText.text = "Congratulations!" + "\n" + "Your final score is:  " + finalScore.ToString();
    }

    public void resetGame(){
        FindObjectOfType<GameSession>().ResetGameSession();
    }
}
