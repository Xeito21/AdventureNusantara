using UnityEngine;

public class NPC : MonoBehaviour
{
    private GameObject player;
    private Vector3 initialScale;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (player != null)
        {
            if (transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }
            else
            {
                transform.localScale = initialScale;
            }
        }
    }
}
