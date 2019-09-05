using UnityEngine;
using System.Collections;

public class bulletControl : MonoBehaviour {
    public Vector2 speed;
    public float delay;

    Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = speed;
        Destroy(gameObject, delay);

    }
        
	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = speed;
	
	}



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("sidewall"))
        {
            
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("enemybullet") || other.gameObject.CompareTag("waterbullet"))
        {
            //print("hi");
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>(), true);
        }

    }





}
