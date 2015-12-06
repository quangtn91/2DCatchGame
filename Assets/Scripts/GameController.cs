using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject ball;
	public GameObject bomb;
	public float timeLeft;
	public Text TimeLeft;
	public GameObject GameOver;
	public GameObject PlayAgain;
	public GameObject StartButton;
	public GameObject SplashScreen;
	public HatController hatController;

	private float maxWidth;
	private bool isPlay = false;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		TimeLeft = GameObject.Find ("TimeLeft").GetComponent<Text> ();
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = ball.GetComponent<Renderer> ().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		UpdateTimeText ();
	}

	void FixedUpdate(){
		if (isPlay) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			UpdateTimeText ();
		}
	}

	public void StartGame(){
		StartButton.SetActive (false);
		SplashScreen.SetActive (false);
		hatController.ToggleCamControl (true);
		StartCoroutine (Spawn ());
	}
	
	IEnumerator Spawn(){
		yield return new WaitForSeconds (2f);
		isPlay = true;
		while (timeLeft > 0) {
			Vector3 spawnPosition = new Vector3(
				Random.Range(-maxWidth, maxWidth),
				gameObject.GetComponent<Transform>().transform.position.y,
				0.0f
				);
			if (Random.Range(0, 100) < 70)
				Instantiate(ball, spawnPosition, Quaternion.identity);
			else
				Instantiate(bomb, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(1f, 2f));
		}
		yield return new WaitForSeconds (1f);
		GameOver.SetActive (true);
		yield return new WaitForSeconds (1f);
		PlayAgain.SetActive (true);
	}

	void UpdateTimeText(){
		TimeLeft.text = "Time left:\n" + Mathf.RoundToInt (timeLeft).ToString();
	}
}
