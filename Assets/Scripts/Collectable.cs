using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public int pointsToAdd;

	void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player") {
            ScoreManager.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
