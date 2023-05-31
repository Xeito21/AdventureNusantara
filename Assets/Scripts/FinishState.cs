using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    public GameObject finishPanel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            finishPanel.SetActive(true);
        }
    }
}
