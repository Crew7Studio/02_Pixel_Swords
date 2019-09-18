using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    LevelManager lvlManager;

    void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            lvlManager.RespawnPlayer();
        }
    }
}
