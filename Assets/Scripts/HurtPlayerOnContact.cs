using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour {

    public int damageToGive;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
     ///       other.GetComponent<AudioSource>().Play();
            HealthManager.HurtPlayer(damageToGive);

            var player = other.GetComponent<PlayerController>();

            player.knockbackCount = player.knockbackLength;

            if (other.transform.position.x < transform.position.x) // if player on the LEFT of enemy
                player.knockFromRight = true;
            else
                player.knockFromRight = false;
        }
    }
}
