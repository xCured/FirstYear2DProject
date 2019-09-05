using UnityEngine;
using System.Collections;

public class wallElementScript : MonoBehaviour
{
    // public GameObject theplayer;
   // public float test;
    // Use this for initialization

    public bool rockwall = false;
    public bool treewall = false;
    public bool metalwall = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

       // test = GetComponent<PlayerManager>().health;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (rockwall == true)
        {
            if (other.gameObject.CompareTag("waterbullet"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                if (other.gameObject.CompareTag("Player") == false && other.gameObject.CompareTag("enemy") == false)
                {
                    Destroy(other.gameObject);
                }
            }

        }

        if (treewall == true)
        {
            if (other.gameObject.CompareTag("firebullet"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                if (other.gameObject.CompareTag("Player") == false && other.gameObject.CompareTag("enemy") == false)
                {
                    Destroy(other.gameObject);
                }
            }

        }

        if (metalwall == true)
        {
           
           
            if (other.gameObject.CompareTag("earthmelee"))
            {
        
                
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                if (other.gameObject.CompareTag("Player") == false && other.gameObject.CompareTag("enemy") == false)
                {
         
                    Destroy(other.gameObject);
                }
            }

        }

        
           



    }
}
