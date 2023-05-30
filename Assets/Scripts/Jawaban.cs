using UnityEngine;

public class Jawaban : MonoBehaviour
{
    public bool isBenar = false;
    public QuestionManager questionManager;
    public void JawabBtn()
    {
        if (isBenar)
        {
            Debug.Log("benar");
            questionManager.Benar();
        }
        else
        {
            Debug.Log("salah");
            questionManager.Salah();
        }
    }
}
