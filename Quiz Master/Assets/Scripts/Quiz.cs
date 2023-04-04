using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answers;
    int correctAnswerIndex;
    bool hasAnswerEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isCompleted = false;

    void Start()
    {
        getInitialQuestion();
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update(){
        timerImage.fillAmount = timer.getFillFraction();
        if(timer.loadNextQuestion){
            if(progressBar.value >= progressBar.maxValue){
                isCompleted = true;
                return;
            }
            hasAnswerEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!timer.getIsAnswering() && !hasAnswerEarly){
            setButtonState(false);
            displayAnswer(-1);
        }
    }

    void displayQuestion(){
        questionText.text = currentQuestion.getQuestion();
        correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.getCorrectAnswer(i);
        }
    }

    public void onAnswerSelected(int index){
        hasAnswerEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
        scoreText.text = "Score : " + scoreKeeper.getScore().ToString() + "%";
    }

    void displayAnswer(int index){
        Image buttonImage;
        if(index == correctAnswerIndex){
            buttonImage = answers[index].GetComponent<Image>();
            questionText.text = "Correct!";
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.incrementCorrectAnswers();
        }
        else{
            buttonImage = answers[correctAnswerIndex].GetComponent<Image>();
            string correctAnswer = currentQuestion.getCorrectAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was\n" + correctAnswer;
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void setButtonState(bool state){
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponent<Button>().interactable = state;
        }
    }

    void getNextQuestion(){
        if(questions.Count > 0){
            getRandomQuestion();
            displayQuestion();
            progressBar.value++;
            setButtonState(true);
            setDefaultButtonSprite();
            scoreKeeper.incrementQuestionsSeen();
        }
    }

    void getInitialQuestion(){
        int randomIndex = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];
        progressBar.value = 0;
    }

    void getRandomQuestion()
    {
        int randomIndex = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];
        if(questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
    }

    void setDefaultButtonSprite(){
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

}
