using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tombol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	}
		public void scale(float scale){
			transform.localScale = new Vector2(1/scale,1*scale);
		}
		public void scene(string scene){
			SceneManager.LoadScene (scene);
		}
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
