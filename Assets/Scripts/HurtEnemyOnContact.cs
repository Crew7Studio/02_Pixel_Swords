using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyOnContact : MonoBehaviour {

    public int damageToGive;
    public float bounceOnEnemy;

    private Rigidbody2D myRigidbody2D;

    void Start()
    {
        //parent was here
        myRigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       // EnemyHealthMechanics();
    }

    //void OnTriggerEnter2D(Collider2D other)
 //   {
   //     if (other.tag == "Enemy") {
            
    //    }
  //  }

    public void EnemyHealthMechanics(GameObject other) {
        // GetComponent<AudioSource>().Play();
        other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, bounceOnEnemy * Time.deltaTime);
    }
}
