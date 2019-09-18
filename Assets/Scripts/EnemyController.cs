using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float moveSpeed;
    public bool moveRight;
    public float sizeX, sizeY;

    Rigidbody2D enemy;

    // wall checking
    public Transform wallChecker;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public bool walled;

    // checking if the ground has run out
    public bool notAtEdge;
    public Transform edgeChecker;



    void Start()
    {

        moveSpeed = 80;
        moveRight = true;

        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        walled = Physics2D.OverlapCircle(wallChecker.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(edgeChecker.position, wallCheckRadius, whatIsWall);

        if (walled || !notAtEdge)
            moveRight = !moveRight;

        if (moveRight)
        {
            // flip enemy based on side if moving RIGHT
            enemy.transform.localScale = new Vector3(sizeX, sizeY, 1f);
            enemy.velocity = new Vector2(moveSpeed * Time.deltaTime, enemy.velocity.y);
        }
        else
        {
            // if moving LEFT
            enemy.transform.localScale = new Vector3(sizeX * -1, sizeY, 1f);
            enemy.velocity = new Vector2(-moveSpeed * Time.deltaTime, enemy.velocity.y);
        }


    }
}
