using UnityEngine;
using System.Collections;

public class HT_Restart : MonoBehaviour {

	public void OnMouseDown () {
		//Application.LoadLevel (Application.loadedLevel);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);    
	}
}
