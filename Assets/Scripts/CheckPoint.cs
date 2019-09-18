using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public LevelManager levelmanager;

    // Use this for initialization
    void Start()
    {
        levelmanager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            // when a palyer passes to any check points this checkpoint gameobj is passed to levelmanager so that the player can
            // respawn in that positin
            levelmanager.currentCheckPoint = gameObject;
           // Debug.Log("Activated Checkpoint " + transform.position);
        }
    }
}
