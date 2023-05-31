using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI titleText;
    public Question[] quizArray;
    public string[] multiChoice;
    public TextMeshProUGUI[] multiChoiceText;
    public int indexQuiz = 0;
    public int answerKey;
    public GameObject correctAnswer;
    public GameObject doneQuiz;
    public GameObject retryButton;
    public TextMeshProUGUI doneQuizText;
    public int maxQuestion=1;
    public int correctScore = 0;
    public TextMeshProUGUI correctAnswerText;

    void start()
    {
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        titleText.text = quizArray[indexQuiz].tittleQuiz;
        questionText.text = quizArray[indexQuiz].question;
        multiChoice = quizArray[indexQuiz].multiChoice;
        answerKey = quizArray[indexQuiz].answer;
        
        for(int i = 0; i < multiChoice.Length; i++)
        {
            multiChoiceText[i].text=  multiChoice[i];
        }
    }

    public void MultiChoiceOnClick(int answerIndex) 
    {
        correctAnswer.gameObject.SetActive(true);
        indexQuiz++;

        if(answerIndex == answerKey)
        {
            correctAnswerText.text = "Jawaban anda benar!!";
            correctScore++;
        }
        else
        {
            correctAnswerText.text = "Jawaban anda salah!!";
        }
    }

    public void AfterChoice()
    {
        if(indexQuiz < maxQuestion)
        {
            GenerateQuestion();
        }
        else
        {
            doneQuiz.SetActive(true);

            if(correctScore == maxQuestion)
            {
                doneQuizText.text = "Selamat Kuis Selesai dengan jawaban Benar semua!";
                retryButton.SetActive(false);
            }
            else
            {
                doneQuizText.text = "Maaf terdapat soal dengan jawaban yang salah, coba lagi?";
                retryButton.SetActive(true);
            }
        }
    }

    public void Retry(){
        doneQuiz.SetActive(false);
        correctAnswer.SetActive(false);  
        indexQuiz = 0;
        correctScore = 0;
        GenerateQuestion();
    }
}
