using UnityEngine;

public class Jawaban : MonoBehaviour
{
    public bool isBenar = false;
    public QuestionManager questionManager;
    public void JawabBtn()
    {
        if (isBenar)
        {
            questionManager.Benar();
        }
        else
        {
            questionManager.Salah();
        }
    }
}
