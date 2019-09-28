using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float playerVelocity;
    public float moveSpeed;
    public float jumpSpeed;
    bool faceingToRight;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    bool grounded;
    bool doubleJump;


    public Transform wallCheck;
    public LayerMask whatIsWall;
    bool walled;
    bool wallSliding;
    public float wallSlideSpeedLimit;

    public Vector2 wallJumpClimb;


    public float slidingForce;
    public bool isSliding;     // sliding on the ground
    public float slidingLength;
    float slidingCounter;

    Rigidbody2D player;
    Animator anim;
    EnemyController enemy;

    RaycastHit2D ray;
    public Transform startRay, endRay;
    public LayerMask whatIsEnemy;
    bool inRange;
    bool isAttacking1;

    public float attackClick_EffectDelay;


    // for knocking back the player
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;




    //combo code 
    public int noOfClicks = 0;
    //Time when last button was clicked
    float lastClickedTime = 0;
    //Delay between clicks for which clicks will be considered as combo
    public float maxComboDelay = 1;

    HurtEnemyOnContact hurtEnemyOnContact;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        knockbackCount = 0;
        slidingCounter = -1;


        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);

        hurtEnemyOnContact = GetComponent<HurtEnemyOnContact>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        walled = Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, whatIsWall);

       
    }

    void Update() {

       

        //WALKING
        playerVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");

        if (knockbackCount <= 0){
            player.velocity = new Vector2(playerVelocity, player.velocity.y);
        }
        else{
            if (knockFromRight){
                player.velocity = new Vector2(-knockback, knockback);
                player.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (!knockFromRight) { 
                player.velocity = new Vector2(knockback, knockback);
                player.transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            knockbackCount -= Time.deltaTime;
        }

        if (player.velocity.x > 0)
        {
            player.transform.localScale = new Vector3(1f, 1f, 1f);
            faceingToRight = true;
        }
        if (player.velocity.x < 0)
        {
            player.transform.localScale = new Vector3(-1f, 1f, 1f);
            faceingToRight = false;
        }


        //WALKING ANIMATION
        anim.SetFloat("Speed", Mathf.Abs(player.velocity.x));


        // JUMPING
        if (grounded)
        {
            doubleJump = false;
            wallSliding = false;
        }

        //JUMP ANIMATION
        anim.SetBool("Grounded", grounded);

        //FALLING ANIMATION
        anim.SetFloat("Falling", player.velocity.y);


        if (Input.GetButtonDown("Jump") && grounded && !wallSliding)
        {
            Jump();
        }
        if (Input.GetButtonDown("Jump") && !grounded && !doubleJump && !wallSliding)
        {
            Jump();
            doubleJump = true;
        }


        //WALLCLIMB
        if (walled)
        {
            //  grounded = false;
            doubleJump = false;
        }

        //WALL SLIDING MECHANICS
        if (faceingToRight && Input.GetAxisRaw("Horizontal") > .1f || !faceingToRight && Input.GetAxisRaw("Horizontal") < -.1f)
        {
            if (walled && !grounded && player.velocity.y < 0)
            {
                wallSliding = true;
                WallMechanics();
            }
            else
                wallSliding = false;
        }


        //WALLSLIDE ANIMATION
        if (walled && player.velocity.y < 0)
        {
            anim.SetBool("WallSlide", true);
            anim.SetFloat("Falling", 0);
        }
        else
        {
            anim.SetBool("WallSlide", false);
        }


        //SLIDING on the ground
        if (Mathf.Abs(playerVelocity) > 1 && Input.GetKeyDown(KeyCode.LeftShift) && grounded) {
            slidingCounter = slidingLength;
        }if (Input.GetKeyUp(KeyCode.LeftShift))
            slidingCounter = 0;

        slidingCounter -= Time.deltaTime;

        if (slidingCounter > 0) {
            Physics2D.IgnoreLayerCollision(8, 11, true);
            Physics2D.IgnoreLayerCollision(8, 13, true);
            isSliding = true;
            anim.SetBool("Sliding", true);
            player.velocity = new Vector2(playerVelocity * slidingForce, player.velocity.y);

        }
        else if(slidingCounter <= 0) {
            Physics2D.IgnoreLayerCollision(8, 11, false);
            Physics2D.IgnoreLayerCollision(8, 13, false);
            anim.SetBool("Sliding", false);
            isSliding = false;
        }

        /*
        //ATTACKING
        if (anim.GetBool("Attack1"))
        {
            anim.SetBool("Attack1", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking1 = true;
            anim.SetBool("Attack1", true);
            StartCoroutine("OnClickFire1Co");
        }

        Debug.DrawLine(startRay.position, endRay.position, Color.green);
        inRange = Physics2D.Linecast(startRay.position, endRay.position, whatIsEnemy); //1<<LayerMask.NameToLayer("Enemy"));
        ray = Physics2D.Linecast(startRay.position, endRay.position, whatIsEnemy); 

        if (inRange)
        {
            Debug.Log(ray.transform.gameObject.tag);
        }

    */


        if (Input.GetButtonDown("Fire1")) {
            OnClick();
            isAttacking1 = true;
            StartCoroutine("OnClickFire1Co");
            GetComponent<AudioSource>().Play();

        }

        //combo code testing>>>>>>>>>>>>>>>>>>>
        if (Time.time - lastClickedTime > maxComboDelay) {
            noOfClicks = 0;

            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", false);
            anim.SetBool("Attack3", false);
        }


        Debug.DrawLine(startRay.position, endRay.position, Color.green);
        inRange = Physics2D.Linecast(startRay.position, endRay.position, whatIsEnemy); //1<<LayerMask.NameToLayer("Enemy"));
        ray = Physics2D.Linecast(startRay.position, endRay.position, whatIsEnemy);

        if (inRange) {
          //  Debug.Log(ray.transform.gameObject.tag + Time.time);
        }




    }  //  END OF UPDATE()

   

    //jumping mechanics
    public void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, jumpSpeed);
    }


    //wall sliding and wall jumping mechanics
    public void WallMechanics()
    {
        //  player.velocity = new Vector2(player.velocity.x, -0.7f);

        // so that the wall sliding velocity will be consitent
        if (player.velocity.y < -wallSlideSpeedLimit)
            player.velocity = new Vector2(player.velocity.x, -wallSlideSpeedLimit);

        if (Input.GetButtonDown("Jump"))
        {
            if (wallSliding = true)
            {
                if (Input.GetAxisRaw("Horizontal") > .1f && faceingToRight)
                {
                    player.velocity = new Vector2(-.9f * wallJumpClimb.x, wallJumpClimb.y);
                }
                if (Input.GetAxisRaw("Horizontal") < -.1f && !faceingToRight)
                {
                    player.velocity = new Vector2(wallJumpClimb.x, wallJumpClimb.y);
                }
            }
        }
    }



    //attacking mechanics
    public System.Collections.IEnumerator OnClickFire1Co()
    {
        if (inRange && isAttacking1 && ray.collider.gameObject.tag == "Enemy")
        {//transform.gameObject.tag == "Enemy"){
            GameObject enemy = ray.collider.gameObject;
            float tempMoveSpeed = enemy.GetComponent<EnemyController>().moveSpeed;


            //ANIMATION enemy taking damage effect
            enemy.GetComponent<Animator>().SetTrigger("hitTrigger");
            enemy.GetComponent<EnemyController>().moveSpeed = 0f;       // slow enemy speed on hit
            enemy.GetComponent<Animator>().SetTrigger("unHitTrigger");

            yield return new WaitForSeconds(attackClick_EffectDelay);

            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().moveSpeed = tempMoveSpeed;

                hurtEnemyOnContact.EnemyHealthMechanics(enemy);
            }
            
            //Destroy(ray.transform.gameObject);
           // ray.transform.gameObject.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
          //  player.velocity = new Vector2(player.velocity.x, bounceOnEnemy * Time.deltaTime);
        }
        isAttacking1 = false;
    }



    //combo attack code 
    //Call on button click
    void OnClick()
    {
        //Record time of last button click
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            //anim.SetBool("Attack1", true);
            anim.SetTrigger("attack1");
        }
        //limit/clamp no of clicks between 0 and 3 because you have combo for 3 clicks
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }
} //END OF CLASS PLAYERCONTROLLER.
