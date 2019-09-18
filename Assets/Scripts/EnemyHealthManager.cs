using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {


    public int enemyHealth;
    public int pointsOnDeath;


  //  public GameObject deathEffect;

    void Start() {
    }

    void FixedUpdate() {

        if (enemyHealth <= 0) {
            //Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(gameObject);
        }
    }

    public void giveDamage(int damageToGive) {
        enemyHealth -= damageToGive;
       // Debug.Log(enemyHealth);
    }
}
