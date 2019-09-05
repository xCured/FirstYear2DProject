using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour {



    //sounds

    public AudioSource damagetaken;
    public AudioSource fireproj;
    public AudioSource waterproj;
    public AudioSource windproj;
    public AudioSource interact;
    public AudioSource jump;
    public AudioSource pickup;
    public AudioSource punch;


    //player variables
    private Rigidbody2D rb2d;
    //private bool jump = true;
    private bool doublejump = true;
    public float JumpHeight;
    public float flyHeight;
    public float speed;
    public float originalspeed;
    
    //projectiles variables
    Transform firePos;
    public GameObject leftBullet, rightBullet,melee;
    public GameObject firebulletleft, firebulletright, firemelee;
    public GameObject waterbulletleft, waterbulletright, watermelee;
    public GameObject earthmelee;

    //UI variables
    public Image healthbarimage;
    public Image emblem;
    public float health = 10.0f;
    private float maxhealth;

    private bool outofcombat = true;
    private float outofcombatduration = 5.0f;
    private float outofcomTimestamp = 0;

    public int chargeamount = 3;
    public Sprite emptycharge;
    public Sprite charged;
    public GameObject[] chargesArray;
    public ArrayList chargesArray2;

    public GameObject charge1;
    public GameObject charge2;
    public GameObject charge3;
    public GameObject charge4;
    public GameObject charge5;


    //player variables
    public Image shieldbarimage;
    private float shield = 0f;
    public float maxshield = 5.0f;


    private int movRight = -1;
    private bool facingright=true;

    public float timebetweenmelee = 1f;
    private float timestampmelee = 0f;

    private bool hit = false;
    private float timestamphit;
    public float timehitdelay;

    private Vector3 spawn;

    private float spawnX = -6;
    private float spawnY =-0.62f;


    // air =1 water=2 earch =3 fire=4
    // water = 1 earth = 2 air =3 fire = 4
   
        //unlocks
    public bool waterUnlocked = false;
    public bool earthUnlocked = false;
    public bool airUnlocked = false;
    public bool fireUnlocked = false;

    private int element = 0;
    public int maxelement = 4;


    //rods
    public bool bluerod = false;
    public bool greenrod = false;
    public bool whiterod = false;
    public bool redrod = false;

    public GameObject canvas;

    //animation
    Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        spawn = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        firePos= transform.FindChild("firePos");
        originalspeed = speed;
        maxhealth = health;
        chargesArray = GameObject.FindGameObjectsWithTag("Charge");
        chargesArray[0] = charge1;
        chargesArray[1] = charge2;
        chargesArray[2] = charge3;
        chargesArray[3] = charge4;
        chargesArray[4] = charge5;


    }

   

    void FixedUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string SceneName = currentScene.name;
        if (SceneName == "menuscreen")
        {
            Destroy(this.gameObject);
        }




        if (outofcombat == true)
        {
            health = health + 0.0005f;
            if (health > maxhealth)
            {
                health = maxhealth;
            }
        }

        if (Time.time > outofcomTimestamp)
        {
            outofcombat = true;
        }
        

        if (element != 2)
        {
            shield = 0;
        }

        for (int i = 0; i < chargesArray.Length; i++)
        {
            if (i <= chargeamount - 1)
            {

                //chargesArray[chargesArray.Length - i - 1].GetComponent<Animator>().enabled = true;
                //chargesArray[chargesArray.Length - i - 1].gameObject.GetComponent<SpriteRenderer>().sprite = charged;

                chargesArray[i].GetComponent<Animator>().enabled = true;
                chargesArray[i].gameObject.GetComponent<SpriteRenderer>().sprite = charged;


            }
            else
            {
                //chargesArray[chargesArray.Length - i - 1].gameObject.GetComponent<SpriteRenderer>().sprite = emptycharge;
                //chargesArray[chargesArray.Length - i - 1].GetComponent<Animator>().enabled = false;

                chargesArray[i].gameObject.GetComponent<SpriteRenderer>().sprite = emptycharge;
                chargesArray[i].GetComponent<Animator>().enabled = false;

            }

        }




        //updates healthbar image
        float healthpercentage = health / maxhealth;
        healthbarimage.fillAmount = healthpercentage;
        //updates shield bar
        float shieldhpercentage = shield / maxshield;
        shieldbarimage.fillAmount = shieldhpercentage;


        //0 = left    1 = right    -1 =  standing

        // if the player is moveing right then movRight = 1
        if (movRight == 1)
        {
            // takes the transform position the player gameobject and put it into a variable called temp
            Vector3 temp = transform.position;
            // increase the variable temp by the speed
            temp.x = temp.x + speed;
            // set the player gameobject position to the variable created which is temp
            transform.position = temp;
            // changes facingright to true thus telling us that the player is facing right
            facingright = true;
        }

        // checks if player is moving left since the movRight variable will be 0
        if (movRight == 0)
        {
            // gets the position of the player gameobject into a variable called temp
            Vector3 temp = transform.position;
            // decrease the temp variable by speed
            temp.x = temp.x - speed;
            // set the position of the player gameobeject to the temp variable
            transform.position = temp;
            // changes the facingright variable to false thus telling us that the player is facing left
            facingright = false;
        }

        //limit the y velocity 
        if (rb2d.velocity.y < 0)
        {
            Vector3 temp = transform.position;
            if(transform.position.y < -7)
            {
                rb2d.Sleep();
                transform.position = spawn;
                health = maxhealth;
                chargeamount = 5;
            }

        }

        if (rb2d.velocity.y >7)
        {
            rb2d.velocity = rb2d.velocity.normalized * 7;

        }

        if (health <= 0)
        {
            // stops the enemy knockback when respawns
            transform.position = spawn;
            rb2d.Sleep();
           
            health = maxhealth;
            chargeamount = 5;


        }



    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            interact.Play();
        }



            if (element == 3)
        {
            emblem.sprite = Resources.Load<Sprite>("Emblems/airEmblem") as Sprite;
        }
        else if (element == 1)
        {
            emblem.sprite = Resources.Load<Sprite>("Emblems/waterEmblem") as Sprite;
        }
        else if (element == 2)
        {
            emblem.sprite = Resources.Load<Sprite>("Emblems/earthEmblem") as Sprite;
        }
        else if (element == 4)
        {
            emblem.sprite = Resources.Load<Sprite>("Emblems/fireEmblem") as Sprite;
        }
        else
        {
            emblem.sprite = Resources.Load<Sprite>("Emblems/emptyEmblem") as Sprite;
        }



     


        // checks if the current time is greater than the time stamp when that player got hit
        if (Time.time > timestamphit)
        {
            hit = false;
        }

        // checks if hit is false so that it prevent the player from doing any action when hit
        // this prevent the counter action between the playing being knocked back and moving since the player can't move when they get hit
        if (hit == false)
        {
            if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0)
            {
                anim.SetInteger("state", 0);
            }

            //changing element with the V key
            if (Input.GetKeyDown(KeyCode.V)){
                if (maxelement > 0)
                {
                    element = element + 1;
                }
                
                if (element > maxelement )
                {
               

                    element = 1;
                   
                }

                if (element == 3)
                {
                    speed = speed * 1.2f;
                }
                else
                {
                    speed = originalspeed;
                }
            }

           

            //check if you press spacebar for glide a
            if (Input.GetKey(KeyCode.Space))
            {
                // checks if you are descending thus meaning velocity will be a negative number and if you are using the wind element
                if (rb2d.velocity.y < 0 && element==3)
                {
                    rb2d.gravityScale = 0.2f;
                    doublejump = false;
                }

            }
            // this changes the gravity back to normal
            else
            {
                rb2d.gravityScale = 1.0f;
            }

            //0 = left -- 1 = right -- -1 =  standing

            //checks if the player is pressing the right arrow key
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) == false)
            {
                anim.SetInteger("state", 1);
                //checks if the player object is facing left and if so change the scale by multiplying it by -1 to flip it
                if (facingright == false)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x = theScale.x * -1;
                    transform.localScale = theScale;
                    facingright = true;
                }
                
                movRight = 1;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                anim.SetInteger("state", 0);
                movRight = -1;
            }


            // checks if the player is pressing the left arrow.
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false)
            {
                anim.SetInteger("state", 1);
                //checks if the player object is facing right which the player press left arrow thus needs to change the way the player is facing
                if (facingright == true)
                {
                    // gets the player scale and put it into theScale variavle
                    Vector3 theScale = transform.localScale;
                    //reverse theScale variable so that it is facing the other way
                    theScale.x = theScale.x * -1;
                    //then set the player scale to the "theScale" variable that you just reversed
                    transform.localScale = theScale;
                    // changed facing right to false
                    facingright = false;
                }
                
                movRight = 0;

            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetInteger("state", 0);
                movRight = -1;
            }

            // detecting if the arrow key is pressed allowing the user to jump
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //checks if you are already in mid air as jump will be equal to truwe if you are in mid0air
                if (rb2d.velocity.y == 0)
                {
                    //add force upwards
                    jump.Play();
                    rb2d.AddForce(transform.up * JumpHeight);
                    anim.SetInteger("state", 2);


                }
                //checks if you have double jump and you are in the element 1 which is wind
                else if (doublejump == true && element == 3)
                {
                    rb2d.AddForce(transform.up * JumpHeight);
                    doublejump = false;
                }

            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                outofcombat = false;
                outofcomTimestamp = Time.time + outofcombatduration;

                if (element > 0)
                {
                    if (chargeamount > 0)
                    {

                        anim.SetInteger("state", 3);
                        Fire();

                        chargeamount = chargeamount - 1;

                    }
                }

            }


            if (Input.GetKeyDown(KeyCode.Z) && Time.time >= timestampmelee)
            {
                punch.Play();
                anim.SetInteger("state", 4);
                outofcombat = false;
                outofcomTimestamp = Time.time + outofcombatduration;

                meleeAttack();
                timestampmelee = Time.time + timebetweenmelee;
                

            };

        }
       





        }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //end level portal------------------------------------------------------------------------------------------>
        if (other.gameObject.name == "endturtorialportal")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                //load scene
                Application.LoadLevel("portal");
                transform.position = new Vector3(26, 2.4f, -1);
                spawn = new Vector3(26, 2.4f, -1);
                waterUnlocked = true;
                rb2d.Sleep();
                canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Portal Background") as Sprite;

            }
        }

        if (other.gameObject.name == "endLevel1portal")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                //load scene
                Application.LoadLevel("portal");
                transform.position = new Vector3(26, 2.4f, -1);
                spawn = new Vector3(26, 2.4f, -1);
                earthUnlocked = true;
                rb2d.Sleep();
                canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Portal Background") as Sprite;

            }
        }

        if (other.gameObject.name == "endLevel2portal")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                //load scene
                Application.LoadLevel("portal");
                transform.position = new Vector3(26, 2.4f, -1);
                spawn = new Vector3(26, 2.4f, -1);
                airUnlocked = true;
                rb2d.Sleep();
                canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Portal Background") as Sprite;

            }
        }
        if (other.gameObject.name == "endLevel3portal")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                //load scene
                Application.LoadLevel("portal");
                transform.position = new Vector3(26, 2.4f, -1);
                spawn = new Vector3(26, 2.4f, -1);
                fireUnlocked = true;
                rb2d.Sleep();
                canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Portal Background") as Sprite;

            }
        }

        if (other.gameObject.name == "endLevel4portal")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                //load scene
                Application.LoadLevel("portal");
                transform.position = new Vector3(26, 2.4f, -1);
                spawn = new Vector3(26, 2.4f, -1);
                rb2d.Sleep();
                canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Portal Background") as Sprite;

            }
        }

        //level portal------------------------------------------------------------------------------------------>


        if (other.gameObject.name == "Level1portal")
        {
            if (waterUnlocked==true && maxelement >= 1)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    //load scene
                    rb2d.Sleep();
                    Application.LoadLevel("sand");
                    transform.position = new Vector3(-4.8f, 0.3f, -1);
                    spawn = new Vector3(-4.8f, 0.3f, -1);
                    canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Coast Background") as Sprite;
                }

            }
           
        }

        if (other.gameObject.name == "Level2portal")
        {
            if (earthUnlocked == true && maxelement >= 2)
            {
               if (Input.GetKeyDown(KeyCode.C))
                {
                    //load scene
                    Application.LoadLevel("grass");
                    transform.position = new Vector3(-9.3f, 0.07f, -1);
                    spawn = new Vector3(-9.3f, 0.07f, -1);
                    rb2d.Sleep();
                    canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Grassy Background") as Sprite;
                }
            }
            
        }

        if (other.gameObject.name == "Level3portal" )
        {
            if (airUnlocked == true && maxelement >= 3)
            {
               if (Input.GetKeyDown(KeyCode.C))
                {
                    //load scene
                    Application.LoadLevel("mountain");
                    transform.position = new Vector3(-8, 4, -1);
                    spawn = new Vector3(-8, 4, -1);
                    rb2d.Sleep();
                    canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Mountains Background") as Sprite;
                }
            }

           
        }

        if (other.gameObject.name == "Level4portal" )
        {
            if (fireUnlocked == true && maxelement >= 4)
            {
               if (Input.GetKeyDown(KeyCode.C))
                {
                    //load scene
                    Application.LoadLevel("volcano");
                    transform.position = new Vector3(34, 6, -1);
                    spawn = new Vector3(34, 6, -1);
                    rb2d.Sleep();

                    //change background image
                    canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/volcanobg") as Sprite;
                }
            }

            
        }

        //if (other.gameObject.name == "endgameportal")
        //{
            
        //        if (Input.GetKeyDown(KeyCode.C))
        //        {
        //            //load scene
        //            Application.LoadLevel("EndScene");
        //            transform.position = new Vector3(0, 0, -1);
        //            spawn = new Vector3(0, 0, -1);
        //        }
            


        //}


        //test porals------------------------------------------------------------------------------------------>
        if (other.gameObject.name == "portal1")
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                Application.LoadLevel("test1");
                transform.position = new Vector3(0, 0, -1); 
                spawn = new Vector3(0, 0, -1);
                waterUnlocked = true;

                //change background image
                //canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Tutbackground") as Sprite;


                //waterUnlocked = true;
                //earthUnlocked = true;


            }
        }
        ////Pedestals------------------------------------------------------------------------------------------>
        //if (other.gameObject.name == "PedestalBlue ")
        //{

        //    if (Input.GetKeyDown(KeyCode.C) && bluerod ==true)
        //    {
                


        //    }
        //}


        //pickup
        if (other.gameObject.tag == "pickup")
        {
            pickup.Play();
            chargeamount = chargeamount + 1;
            if (chargeamount > 5)
            {
                chargeamount = 5;
            }

            Destroy(other.gameObject);
        }


    }

    //detects if you hit a checkpoint
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            var spawnpoint = other.transform.position;
            spawn = new Vector3(spawnpoint.x, spawnpoint.y, -1);
        }


    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("watermelee") || coll.gameObject.CompareTag("firemelee") || coll.gameObject.CompareTag("melee") || coll.gameObject.CompareTag("earthmelee"))
        {

            Physics2D.IgnoreCollision(coll.collider, gameObject.GetComponent<Collider2D>(), true);
        }


        if (coll.gameObject.tag == "BlueRod")
        {
            bluerod = true;
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "RedRod")
        {
            redrod = true;
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "GreenRod")
        {
            greenrod = true;
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "WhiteRod")
        {
            whiterod = true;
            Destroy(coll.gameObject);
        }





        if (coll.gameObject.tag == "enemybullet")
        {
            outofcombat = false;
            outofcomTimestamp = Time.time + outofcombatduration;
 
            var force = transform.position - coll.transform.position;
            Destroy(coll.gameObject);

            rb2d.AddForce(force);
            hit = true;
            timestamphit = Time.time + timehitdelay;
            movRight = -1;

            damagetaken.Play();
            healthdecrease();
        }

        if (coll.gameObject.tag == "platform")
        {
                doublejump = true;
        }

        // checks if the player collide with the enemy by using the tag
        if (coll.gameObject.CompareTag("enemy"))
        {
            outofcombat = false;
            outofcomTimestamp = Time.time + outofcombatduration;

            //knock back force
            var force = transform.position - coll.transform.position;
            rb2d.AddForce(force*200);
            hit = true;
            timestamphit = Time.time + timehitdelay;
            movRight = -1;
            damagetaken.Play();
            healthdecrease();

          

        }
    }


    // functio created to fire projectiles
    void Fire()
    {
        // checks which way the player is facing to know which project to instantiate

        if (element == 3)
        {
            windproj.Play();
            if (facingright == true)
            {
                // create the rightbulllter
                Instantiate(rightBullet, firePos.position, Quaternion.identity);
                
            }
            if (facingright == false)
            {
                // create the left bullet
                Instantiate(leftBullet, firePos.position, Quaternion.identity);
            }

        }

        if (element == 4)
        {
            fireproj.Play();
            if (facingright == true)
            {
                // create the rightbulllter
                Instantiate(firebulletright, firePos.position, Quaternion.identity);
            }
            if (facingright == false)
            {
                // create the left bullet
                Instantiate(firebulletleft, firePos.position, Quaternion.identity);
            }

        }

        if (element == 1)
        {
            waterproj.Play();
            if (facingright == true)
            {
                // create the rightbulllter
                Instantiate(waterbulletright, firePos.position, Quaternion.identity);
            }
            if (facingright == false)
            {
                // create the left bullet
                Instantiate(waterbulletleft, firePos.position, Quaternion.identity);
            }

        }

        if (element == 2)
        {
            shield = maxshield;

            float shieldhpercentage = shield / maxshield;
            shieldbarimage.fillAmount = shieldhpercentage;
        }


    }

    void meleeAttack()
    {
        // creates the melee hitbox
        
        if (element == 3 || element == 0)
        {
            Instantiate(melee, firePos.position, Quaternion.identity);
        }

        if (element == 1)
        {
            Instantiate(watermelee, firePos.position, Quaternion.identity);
        }
        if (element == 2 )
        {
            Instantiate(earthmelee, firePos.position, Quaternion.identity);
        }
        if (element == 4)
        {
            Instantiate(firemelee, firePos.position, Quaternion.identity);
        }
    }

    void healthdecrease()
    {
        if (shield > 0)
        {
            shield = shield - 2;
            if (shield < 0)
            {
                health = health + shield;
                shield = 0;
                print(health);
            }
        }
        else
        {
            health = health - 1;
        }
    }








}
