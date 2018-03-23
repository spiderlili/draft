using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBehaviour : MonoBehaviour {

	public GameObject player;
	public GameObject blackOutPanel; 
	public GameObject whiteOutPanel; 

	public bool rotatingLeft = false;
	public bool rotatingRight = false;
	public float rotateTarget;
	public float rotateRemaining;
	public int backgroundCycle; //cycle through the background pictures

	public Vector3 lockPos;
    //background pictures
	public GameObject backgrounds;
	public GameObject background01;
	public GameObject background02;
	public GameObject background03;
	public GameObject background04;

	public Slider clarityBar;
	public float clarity;
	public float clarityMax;
	public Light worldLight;

	public bool twistHit = false;
    public AudioSource rotateSE;      // Audio clip of the player rotating the world.

    void backgroundFadeIn(GameObject background) //fade in background picture 
	{
		background.GetComponent<Renderer>().material.color = new Color (background.GetComponent<Renderer>().material.color.r, background.GetComponent<Renderer>().material.color.g,
			background.GetComponent<Renderer>().material.color.b, Mathf.Clamp(background.GetComponent<Renderer>().material.color.a + Time.deltaTime, 0, 1));
	}

	void backgroundFadeOut(GameObject background) //fade out background picture
	{
		background.GetComponent<Renderer>().material.color = new Color (background.GetComponent<Renderer>().material.color.r, background.GetComponent<Renderer>().material.color.g,
			background.GetComponent<Renderer>().material.color.b, Mathf.Clamp(background.GetComponent<Renderer>().material.color.a - Time.deltaTime, 0, 1));
	}

	public void retryLevel()
	{
        //Check if we are running either in the Unity editor or in a standalone build.
		SceneManager.LoadScene("MindHole"); //reload the main scene
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        SceneManager.LoadScene("level1Android"); //reload the main scene
#endif
    }

    public void endGame()
	{
		Application.Quit(); //quit the game
	}


	void Start () {
		rotateRemaining = 90;
		backgroundCycle = 1; //default background picture
		lockPos = player.transform.position;
	}

	void FixedUpdate()
	{
		if (rotatingLeft) {
			//SwitchStyle			backgrounds.transform.position = backgrounds.transform.position - (backgrounds.transform.position - new Vector3 (lockPos.x + (player.transform.position.x - lockPos.x) / 1.5f, lockPos.y + (player.transform.position.y - lockPos.y) / 1.5f, backgrounds.transform.position.z)) / 5;
			rotateRemaining = rotateRemaining / 2;
			transform.RotateAround (player.transform.position, Vector3.forward, rotateRemaining);
			if (rotateRemaining <= 5) {
				transform.RotateAround (player.transform.position, Vector3.forward, rotateRemaining);
				rotatingLeft = false;
				twistHit = false;
				rotateRemaining = 90;
			}
		} else if (rotatingRight) {
			//SwitchStyle			backgrounds.transform.position = backgrounds.transform.position - (backgrounds.transform.position - new Vector3 (lockPos.x + (player.transform.position.x - lockPos.x) / 1.5f, lockPos.y + (player.transform.position.y - lockPos.y) / 1.5f, backgrounds.transform.position.z)) / 5;
			rotateRemaining = rotateRemaining / 2;
			transform.RotateAround (player.transform.position, Vector3.back, rotateRemaining);
			if (rotateRemaining <= 5) {
				transform.RotateAround (player.transform.position, Vector3.back, rotateRemaining);
				rotatingRight = false;
				twistHit = false;
				rotateRemaining = 90;
			}
		} else {
			backgrounds.transform.position = backgrounds.transform.position - (backgrounds.transform.position - new Vector3 (lockPos.x + (player.transform.position.x - lockPos.x) / 1.5f, lockPos.y + (player.transform.position.y - lockPos.y) / 1.5f, backgrounds.transform.position.z)) / 5;
		}
	}
		

	void Update () {
	//mobile inputs
	#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		if (Input.GetMouseButtonDown (1) && !rotatingLeft && !rotatingRight) { //left mouse button
			lockPos = player.transform.position;
			rotateRemaining = 90;
			rotatingLeft = true;
			backgroundCycle ++;
            rotateSE.Play();
        }
		if (Input.GetMouseButtonDown (0) && !rotatingRight && !rotatingLeft) { //right mouse button
			lockPos = player.transform.position;
			rotateRemaining = 90;
			rotatingRight = true;
			backgroundCycle--;
            rotateSE.Play();
        }
	#else
  	//web build or desktop build
		if (Input.GetMouseButtonDown (1) && !rotatingLeft && !rotatingRight) { //left mouse button
			lockPos = player.transform.position;
			rotateRemaining = 90;
			rotatingLeft = true;
			backgroundCycle ++;
            rotateSE.Play();
        }
		if (Input.GetMouseButtonDown (0) && !rotatingRight && !rotatingLeft) { //right mouse button
			lockPos = player.transform.position;
			rotateRemaining = 90;
			rotatingRight = true;
			backgroundCycle--;
            rotateSE.Play();
        }		
	#endif	
		if (twistHit) { //if the twister is hit
			lockPos = player.transform.position;
			rotateRemaining = 180;
			rotatingLeft = true;
			backgroundCycle --;
			twistHit = false;
		}

        //cycle through the background pictures - fade in and out
		if (backgroundCycle == 1) {
			backgroundFadeIn (background01);
			backgroundFadeOut (background02);
			backgroundFadeOut (background03);
			backgroundFadeOut (background04);
		} else if (backgroundCycle == 2) {
			backgroundFadeIn (background02);
			backgroundFadeOut (background01);
			backgroundFadeOut (background03);
			backgroundFadeOut (background04);
		} else if (backgroundCycle == 3) {
			backgroundFadeIn (background03);
			backgroundFadeOut (background02);
			backgroundFadeOut (background01);
			backgroundFadeOut (background04);
		} else if (backgroundCycle == 4) {
			backgroundFadeIn (background04);
			backgroundFadeOut (background02);
			backgroundFadeOut (background03);
			backgroundFadeOut (background01);
		} else if (backgroundCycle > 4) {
			backgroundCycle = 1;
		} else if (backgroundCycle < 1) {
			backgroundCycle = 4;
		}

		if (player.GetComponent<PlayerMovement> ().gameOver == true) {
			blackOutPanel.SetActive(true);
		}

		if (player.GetComponent<PlayerMovement> ().winGame == true) {
			whiteOutPanel.SetActive(true);
		}
	}
}
