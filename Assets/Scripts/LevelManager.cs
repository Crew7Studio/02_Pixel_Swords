using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    // here we put the checkpoint gameobject in the unity
    public GameObject currentCheckPoint;    // what point the player is to be respwaned											
    private PlayerController player;        // to know where the player is // player script....

    // for particle effect
    ///public GameObject deathParticle;
  ///  public GameObject spawnParticle;

    public float respawnDelay;      // delay between death and respawning of the player

    public int pointPenaltyOnDeath;
    /*NO NEED FOR GRAVITY SINCE WE ARE USING SEPERATE CAMERA SCRIPTS FOR CONTOLLING THE VIEW!!!*/
    // private float gravityStore; 	// for storing the player graivty
    // public float gravity;

   /// CameraController camera; // camera scripts

    HealthManager healthmanager;

    // Use this for initialization
    void Start()
    {

        // find any obj is scene attached with PlayerController script
        player = FindObjectOfType<PlayerController>();

      ///  camera = FindObjectOfType<CameraController>();

        respawnDelay = 1f;

     ///   pointPenaltyOnDeath = 10;

        healthmanager = FindObjectOfType<HealthManager>();
        //gravityStore = 1.5f;
        //gravity = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RespawnPlayer() {
        // calling the coroutine
        StartCoroutine("RespawnPlayerCo");
    }

    // COROUTINE..... for making delay between player death and the player respawning...
    public IEnumerator RespawnPlayerCo()
    {
        // clones the orginal object and returns the clone
        // ON PLAYER DEATH...
     ///   Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        // we need position and rotation when instantiating an object

        // taking points on death of the player
        ScoreManager.AddPoints(-pointPenaltyOnDeath);

        // storing the orginal player gravity
        // gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;		// NOT WORKING :(

        // camera mustnot follow anything after the player is dead
    ///    camera.isFollowing = false;

        player.enabled = false;         // so that after the player is dead user cannot contol the player;
        player.GetComponent<Renderer>().enabled = false;        // so that the player will be invisible after death in time delay between
                                                                // death and respawning...
                                                                // for completely stopping the player after the death...
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;     // vector2.zero set x==y==0

        // when the player falls the camera dosent go on falling with the palyer
        // player shouldn't bee keeping on falling
        // player.GetComponent<Rigidbody2D>().gravityScale = 0f;	

        //MAKING TIME DELAY___________________________________________________________________________
        yield return new WaitForSeconds(respawnDelay);

        // resetting the orginal palyer graivty
        //player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;		// NOT WORKING I DONT KNOW SHIT!!!!!!!!
        //player.GetComponent<Rigidbody2D>().gravityScale = gravity;

        // RESETTING THE HEALTH OF THE PLAYER
        healthmanager.FullHealth();
        healthmanager.isDead = false;
        // so that the player is not killed over and over again in the limited time

        player.knockbackCount = 0;

        // resume the camera following the player
  ///      camera.isFollowing = true;

        // resetting the setting. i.e so player is visible and can be controlled by the user.
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;



        // on death the player position is changed to the last checkpoint he passed.
        player.transform.position = currentCheckPoint.transform.position;

        // ON PLAYER RESPAWN
   ///     Instantiate(spawnParticle, player.transform.position, player.transform.rotation);
    }


}	// END OF CLASS 
