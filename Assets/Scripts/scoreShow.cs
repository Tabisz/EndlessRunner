using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreShow : MonoBehaviour {
	// Use this for initialization
	private Text scoreText;
	int score;
	void Start () {
		score = ScoreHandler.score;
		scoreText = GetComponent<Text>();
		scoreText.text = "You got\n" + score + "\npoints!";

	}
}
	

