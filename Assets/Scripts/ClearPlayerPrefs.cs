using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    [SerializeField] GameObject clearPanel;
    public void MenghapusData()
    {
        clearPanel.SetActive(true);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
