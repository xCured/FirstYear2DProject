using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {
    public float health = 5;
    private Rigidbody2D rb2d;
    public float speed;
    public GameObject player;
    public int agrorange= 5;

    //burn affect variable
    private bool burnt = false;
    public float burndmg = 0.005f;
    private float burnttimestamp;
    public float burnduaration = 5.0f;

    // water affect variables
    private bool soaked = false;
    private bool slowed = false;
    private bool freeze = false;

    private float soaktimestamp;
    private float slowtimestamp;
    private float freezetimestamp;


    public float slowness = 5.0f;

    public float soakedduration = 5.0f;
    public float slowduration = 3.0f;
    public float freezeduration = 5.0f;

    private float origianlspeed;

    // ranged
    public bool ranged = false;
    public float firetimestamp = 0.0f;
    public float rateOfFire = 2.0f;

    Transform leftpos;
    public GameObject leftBullet, rightBullet;

    public bool playerRight = false;
    public bool facingRight = false;
    private bool playerclose = false;



    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        origianlspeed = speed;
        leftpos = transform.FindChild("leftpos");
    }



    void FixedUpdate()
    {
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }

        Vector3 temp = transform.position;
        temp.x = temp.x + speed;
        transform.position = temp;

        // gets the player object position
        var pos = GameObject.Find("animPlayer").transform.position;

        // calculate the distance between the enemy object position and the player object position
        var dist = transform.position - pos;


        // checks if the player is within 5 distance apart
        // if the dist is positive then the enemy is on the right side of the player since enemy.pos - player.pos will always be positive if the player is on the left
        //player is on the left
        if (dist.x > 0 && dist.x < agrorange)
        {
            playerclose = true;
            if (speed > 0)
            {
                speed = speed * -1;

                Vector3 theScale = transform.localScale;
                theScale.x = theScale.x * -1;
                transform.localScale = theScale;
                playerRight = false;

            }

            if (Time.time > firetimestamp && ranged == true)
            {
                playerRight = false;

                Fire();
                firetimestamp = Time.time + rateOfFire;

            }

        }

        // if they are 5 distance apart and the enemy is moving right then change direction
        // checks if the distance is between 0 to 5
        //player is on the right
        else if (dist.x < 0 && dist.x > -agrorange)
        {
            playerclose = true;
            if (speed < 0)
            {
                playerRight = true;
                speed = speed * -1;
                Vector3 theScale = transform.localScale;
                theScale.x = theScale.x * -1;
                transform.localScale = theScale;
            }

            if (Time.time > firetimestamp && ranged == true)
            {

                playerRight = true;
                Fire();
                firetimestamp = Time.time + rateOfFire;

            }

            // checks if the enemy object is moveing left and if so change the direction of the speed so that it starts moving to the right
            // if dist is between 0 and -5 then we know the enemy is on the left side of the enemy thus meaning we want it to move to the right

        }
        else
        {
            playerclose = false;
        }



        // checks if the current time is less than the burnt time stamp and if so reduce the health by the burn damamge
        if (Time.time < burnttimestamp)
        {
            // checks if the enemy is burnt
            if (burnt == true)
            {
                health = health - burndmg;
            }
        }
        // checks if the current time is greater than the burnt time stamp and if so set burn variable to false
        else if (Time.time > burnttimestamp)
        {
            burnt = false;
        }

        // turn the slowed is true
        if (slowed == true)
        {
            // check if the current time is greater than the slow time stamp
            // if so turn the speed to it's original soeed

            if (Time.time > slowtimestamp)
            {
                slowed = false;
                if (playerRight == true)
                {
                    speed = origianlspeed * -1;
                }
                if (playerRight == false)
                {
                    speed = origianlspeed;
                }
                // speed = origianlspeed;

            }

        }

        // checks if the enemy is soaked
        if (soaked == true)
        {
            // checks if the current time is greater than the soak time stamp and if so turn soaked to false;
            if (Time.time > soaktimestamp)
            {
                soaked = false;
            }

        }

        if (freeze == true)
        {
            if (Time.time > freezetimestamp)
            {
                freeze = false;
                if (playerRight == true)
                {
                    speed = origianlspeed * -1;
                }
                if (playerRight == false)
                {
                    speed = origianlspeed;
                }
            }





        }
    }
	
	// Update is called once per frame
	void Update () {
        //checks if the health is equal or less than 0 and if so destroy the gameObject
        if (health < 0 || health == 0)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform") && playerclose==false)
        {
            playerRight = !playerRight;
            speed = speed * -1;
            Vector3 theScale = transform.localScale;
            theScale.x = theScale.x * -1;
            transform.localScale = theScale;

        }

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        // reverse the speed if the enemy object hits a sidewall
        if (other.gameObject.CompareTag("sidewall"))
        {

            playerRight = !playerRight;
            speed = speed * -1;
            Vector3 theScale = transform.localScale;
            theScale.x = theScale.x * -1;
            transform.localScale = theScale;
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("enemybullet"))
        {
            
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>(),true);
        }

       

        // checks if the enemy object collide with a bullet
        if (other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("firebullet") || other.gameObject.CompareTag("waterbullet"))
        {   
            //knock back force
            var force = transform.position - other.transform.position;
            rb2d.AddForce(force);
        
            Destroy(other.gameObject);
            health = health - 1;
            
            if (health < 0 || health==0){
                Destroy(gameObject);
            }

            // checks if it is a firebullet and if so it will cause it the be burnt and calculate the duration of the burning effect
            if (other.gameObject.CompareTag("firebullet"))
            {
                burnt = true;
                burnttimestamp = Time.time + burnduaration;
            }
            // turns soaked and slowed variable to true and set a time stamp for them
            if (other.gameObject.CompareTag("waterbullet"))
            {
                soaked = true;
                soaktimestamp = Time.time + soakedduration;
                slowed = true;
                slowtimestamp = Time.time + slowduration;

               // reduce the speed by diving it by the slowness
                if (speed > 0)
                {
                    speed = origianlspeed * -1;
                    speed = speed / slowness;
                }
                if (speed < 0)
                {
                    speed = origianlspeed;
                    speed = speed / slowness;
                }



                
                
            }

        }

 

        //checks if the enemy object is hit by a melee attack
        if (other.gameObject.CompareTag("melee") || other.gameObject.CompareTag("watermelee") || other.gameObject.CompareTag("firemelee") || other.gameObject.CompareTag("earthmelee"))
        {
            //knock back force
            var force = transform.position - other.transform.position;
            rb2d.AddForce(force*150);
            
            // destroy the melee object
            Destroy(other.gameObject);
            //reduce health by 1
            health = health - 1;
            // if the enemy health reaches 0 or less than 0 then destroy the enemy gameobject
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            // wind more knockback
            if (other.gameObject.CompareTag("melee"))
            {
                rb2d.AddForce(force * 150);
            }



            // checks if the melee attack was a water melee
            if (other.gameObject.CompareTag("watermelee")){
                // checks if the enemy was soaked
                if (soaked == true)
                {
                    // multiply the speed by 0 so the speed is 0
                    speed = speed * 0;
                    // set freeze variable to true
                    freeze = true;
                    // create a freeze time stamp
                    freezetimestamp = Time.time + freezeduration;
                    // set soaked to false so the player can't spam the melee attack during freeeze which will cause the enemy to keep being freezed
                    soaked = false;
                }
                else
                {
                    // slows the enemy if the attack was a water melee and the enemy wasn't soaked
                    slowed = true;
                    // creates time stamp for slowness
                    slowtimestamp = Time.time + slowduration;
                    // set the speed to orignal then divide by slownees so it won't stack the slowness

                    if (speed > 0)
                    {
                        speed = origianlspeed * -1;
                        speed = speed / slowness;
                    }
                    if (speed < 0)
                    {
                        speed = origianlspeed;
                        speed = speed / slowness;
                    }

                }
            }

            if (other.gameObject.CompareTag("firemelee"))
            {
                burnt = true;
                burnttimestamp = Time.time + burnduaration;
            }

        }

    }


    void Fire()
    {
        if (playerRight == true)
        {
            Instantiate(rightBullet, leftpos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(leftBullet, leftpos.position, Quaternion.identity);
        }

        //Instantiate(leftBullet, leftpos.position, Quaternion.identity);

    }
}
