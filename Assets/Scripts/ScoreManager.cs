using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

    public static int score;
    Text txt;

	void Start () {
        txt = GetComponent<Text>();
        score = 0;
	}
	
	void Update () {
		if(score < 0) {
            score = 0;
        }

        txt.text = " " + score;

	}


    public static void AddPoints(int pointsToAdd) {
        score += pointsToAdd;
    }

    public static void Reset(){
        score = 0;
    }
}
