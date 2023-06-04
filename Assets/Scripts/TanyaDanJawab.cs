
using UnityEngine;

[System.Serializable]
public class TanyaDanJawab
{
    [Header("Membuat Pertanyaan")]
    public string Pertanyaan;

    [Header("Pilihan Jawaban")]
    public string[] Jawaban;

    [Header("Jawaban yang Benar")]
    public int jawabanBenar;
}
