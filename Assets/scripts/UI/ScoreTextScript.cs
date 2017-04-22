using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour {


    private Text text;
    private int score;
    public bool highscore = false;

	void Start () {
        text = GetComponent<Text>();               
    }
	
	// Update is called once per frame
	void Update () {
        score = FindObjectOfType<GameManagerScript>().score;
        if (highscore) score = FindObjectOfType<GameManagerScript>().highScore;
        text.text = score.ToString();
    }
}
