using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI titleText;
    public Question[] questionArray;
    public string[] multiChoice;
    public TextMeshProUGUI[] multiChoiceText;
    public int indexQuestion = 0;
    public int answerQuestion;
    public GameObject correctAnswer;
    public GameObject doneQuiz;
    public GameObject retryButton;
    public TextMeshProUGUI doneQuizText;
    public int maxQuestion=3;
    public int correctScore = 0;
    public TextMeshProUGUI correctAnswerText;

    void start()
    {
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        questionText.text = questionArray[indexQuestion].question;
        multiChoice = questionArray[indexQuestion].multiChoice;
        answerQuestion = questionArray[indexQuestion].answer;
        for(int i = 0; i < multiChoice.Length; i++){
            multiChoiceText[i].text=  multiChoice[i];
        }
    }

    public void MultiChoiceOnClick(int answerIndex){
        correctAnswer.gameObject.SetActive(true);
        indexQuestion++;
        if(answerIndex == answerQuestion){
            //jika Benar
            correctAnswerText.text = "Jawaban Benar!";
            correctScore++;
        }else{
            //Jika Salah            
            correctAnswerText.text = "Jawaban Salah!";
        }
    }

    public void AfterChoice(){
        if(indexQuestion < maxQuestion){
            //next soal
            GenerateQuestion();
        }
        else{
            doneQuiz.SetActive(true);
            if(correctScore == maxQuestion){
                doneQuizText.text = "Selamat Kuis Selesai dengan jawaban Benar semua!";
                retryButton.SetActive(false);
            }else{
                 doneQuizText.text = "Maaf terdapat soal dengan jawaban yang salah, coba lagi?";
                 retryButton.SetActive(true);
            }
            //Kuis selesai
        }
    }

    public void Retry(){
        doneQuiz.SetActive(false);
        correctAnswer.SetActive(false);  
        indexQuestion = 0;
        correctScore = 0;
        GenerateQuestion();
    }
}
