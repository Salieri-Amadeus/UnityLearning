using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public void incrementCorrectAnswers(){
        correctAnswers++;
    }

    public void incrementQuestionsSeen(){
        questionsSeen++;
    }

    public int getCorrectAnswers(){
        return correctAnswers;
    }

    public int getQuestionsSeen(){
        return questionsSeen;
    }

    public int getScore(){
        return Mathf.RoundToInt((float) correctAnswers / (float) questionsSeen * 100);
    }
}
