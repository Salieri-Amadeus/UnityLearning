using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz Question", menuName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter question here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;

    public string getQuestion(){
        return question;
    }

    public int getCorrectAnswerIndex(){
        return correctAnswer;
    }

    public string getCorrectAnswer(int index){
        return answers[index];
    }
}
