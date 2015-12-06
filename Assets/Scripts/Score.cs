using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Text TextScore;

	private int score = 0;

	// Use this for initialization
	void Start () {
		TextScore = GameObject.Find ("TextScore").GetComponent<Text> ();
	}
	
	void OnTriggerEnter2D(Collider2D ball){
		if (ball.tag == "Ball")
			TextScore.text = "Score: " + ++score;
		else if (ball.tag == "Bomb") {
			score -= 2;
			TextScore.text = "Score: " + score;
		}
	}
}
