using UnityEngine;
using System.Collections;

public class HT_GameController : MonoBehaviour {
	
	public Camera cam;
	public GameObject[] balls;
	public float timeLeft;
	public GUIText timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HT_HatController hatController;
	
	private float maxWidth;
	private bool counting;
	
	//spawn objects
	
	// Use this for initialization
	void Start () {
		
		//automatically assign the camera to main camera if unassigned
		
		if (cam == null) {
			cam = Camera.main;
		}
		
		//define the height and width of the screen and convert to world space
		
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
	}

	//consistent update for timer
	void FixedUpdate () {
		//only start counting the timer once the game is active
		if (counting) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
		}
	}

	public void StartGame () {
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		
		//enable control on the hatController when the game starts - initially off
		hatController.ToggleControl (true);

		//coroutine loop for instantiating falling objects
		StartCoroutine (Spawn ());
	}

	
	public IEnumerator Spawn () {
		yield return new WaitForSeconds (0.5f);
		counting = true;
		while (timeLeft > 0) {
			GameObject ball = balls [Random.Range (0, balls.Length)];
			
			//randomise the spawn X position for the falling objects
			Vector3 spawnPosition = new Vector3 (
				transform.position.x + Random.Range (-maxWidth, maxWidth), 
				transform.position.y, 
				0.0f
			);
			
			Quaternion spawnRotation = Quaternion.identity;
			

			Instantiate (ball, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (Random.Range (1.0f, 2.0f));
		}
		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true);
	}
}
