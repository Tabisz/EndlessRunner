using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class scoreShow : MonoBehaviour {
    // Use this for initialization
    [SerializeField]
    private List<Text> scoreText;
    private List<int> scores = new List<int>();

	void Start () {
        for (int i = 0; i < scoreText.Count; i++)
        {
            scores.Add(PlayerPrefs.GetInt("score" + i.ToString(), 0));          
        }
            scores.Sort();
            scores.Reverse();
        for (int i = 0; i < scoreText.Count; i++)
        {
                scoreText[i].text = scores[i].ToString(); 
        }
        

	}
}
	

