using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {
	
	public static int score;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		score = 0;
		UpdateScore ();

	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore(){
		
		text.text = "Score: " + score;
	}


}
