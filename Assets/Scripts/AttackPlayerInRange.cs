using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerInRange : MonoBehaviour {

    PlayerController player;
    Rigidbody2D enemy;

    Animator anim;

    //public float playerInRange;
    public Transform startRay, endRay;
    public bool attackingRange;

    
    public float moveTowardsPlayerInRangeX;
    public float movingRangeYPos;
    public bool movingRange;
    public LayerMask whatIsPlayer;

    bool faceingRight;
    EnemyController enemyController;

    public float attackDelayLength;
    float attackDelayCounter;

    public int damage;

    float tempVelocity;


    void Start () {
        player = FindObjectOfType<PlayerController>();
        enemy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        tempVelocity = enemy.GetComponent<EnemyController>().moveSpeed;
    }

    void Update() {

        //for attacking lines
        // Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z),
        //     new Vector3(transform.position.x + playerInRange, transform.position.y, transform.position.z),
        //   Color.red);
        Debug.DrawLine(startRay.position, endRay.position, Color.red);

        // for following the player lines
        Debug.DrawLine(new Vector3(transform.position.x - moveTowardsPlayerInRangeX, transform.position.y - movingRangeYPos, transform.position.z),
           new Vector3(transform.position.x + moveTowardsPlayerInRangeX, transform.position.y - movingRangeYPos, transform.position.z),
           Color.cyan);

        //checks if the player is in range for moving
        movingRange = Physics2D.Linecast(new Vector2(transform.position.x - moveTowardsPlayerInRangeX, transform.position.y - movingRangeYPos), new Vector2(transform.position.x + moveTowardsPlayerInRangeX, transform.position.y - movingRangeYPos), whatIsPlayer);

        

        //checks if the player is close to attack
        attackingRange = Physics2D.Linecast(startRay.position, endRay.position, whatIsPlayer);

        if (enemy.transform.localScale.x > 0)
            faceingRight = true;
        if (enemy.transform.localScale.x < 0)
            faceingRight = false;




        //code to MOVE TOWARDS THE PLAYER
        //if enemy is moving RIGHT and player is INFRONT of him and is in the range
        if (faceingRight && player.transform.position.x > transform.position.x && movingRange) {// player.transform.position.x < transform.position.x + moveTowardsPlayerInRange) {
            enemyController.moveRight = true;
        }
        //enemy is moving RIGHT and player is BEHIND him and is in the range
        else if (faceingRight && player.transform.position.x < transform.position.x && movingRange) {// player.transform.position.x > transform.position.x - moveTowardsPlayerInRange) {
            enemyController.moveRight = false;
        }


        //enemy is moving LEFT and player is INFRONT of him and is in the range
        if (!faceingRight && player.transform.position.x < transform.position.x && movingRange) {//player.transform.position.x > transform.position.x - moveTowardsPlayerInRange) {
            enemyController.moveRight = false;
        }
        //enemy is moving LEFT and player is BEHIND him and is in the range
        else if (!faceingRight && player.transform.position.x > transform.position.x && movingRange) {// player.transform.position.x < transform.position.x + moveTowardsPlayerInRange) {
            enemyController.moveRight = true;
        }


        if (anim.GetBool("attack"))
            anim.SetBool("attack", false);

        attackDelayCounter -= Time.deltaTime;
        // code for ATTACKING player
        if (attackingRange) {
            enemy.GetComponent<EnemyController>().moveSpeed = 0f;

            if (attackDelayCounter <= 0) {
                anim.SetBool("attack", true);
                attackDelayCounter = attackDelayLength;
            }
        }
        else enemy.GetComponent<EnemyController>().moveSpeed = tempVelocity;

    }//END OF UPDATE()

}
