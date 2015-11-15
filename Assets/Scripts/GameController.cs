using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject ball;
	public float timeLeft;
	public Text TimeLeft;

	private float maxWidth;

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
		StartCoroutine (Spawn ());
		UpdateTimeText ();
	}

	void FixedUpdate(){
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = 0;
		}
		UpdateTimeText ();
	}
	
	IEnumerator Spawn(){
		yield return new WaitForSeconds (2f);
		while (timeLeft > 0) {
			Vector3 spawnPosition = new Vector3(
				Random.Range(-maxWidth, maxWidth),
				gameObject.GetComponent<Transform>().transform.position.y,
				0.0f
				);
			Instantiate(ball, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(1f, 2f));
		}
	}

	void UpdateTimeText(){
		TimeLeft.text = "Time left:\n" + Mathf.RoundToInt (timeLeft).ToString();
	}
}
