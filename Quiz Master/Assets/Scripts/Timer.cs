using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timeValue;
    bool isAnswering = false;
    public bool loadNextQuestion = false;
    float fillFraction;

    void Update()
    {
        updateTimer();
    }

    void updateTimer(){
        timeValue -= Time.deltaTime;
        if(timeValue <= 0){ 
            fillFraction = 1;
            if(isAnswering){
                timeValue = timeToShowCorrectAnswer;
                isAnswering = false;
            }
            else{
                timeValue = timeToCompleteQuestion;
                isAnswering = true;
                loadNextQuestion = true;
            }
        }
        else{
            if(isAnswering){
                fillFraction = timeValue / timeToCompleteQuestion;
            }
            else{
                fillFraction = timeValue / timeToShowCorrectAnswer;
            }
        }
    }

    public void cancelTimer(){
        timeValue = 0;
    }

    public float getFillFraction(){
        return fillFraction;
    }

    public bool getIsAnswering(){
        return isAnswering;
    }
}
