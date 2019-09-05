using UnityEngine;
using System.Collections;

public class healthbarsize : MonoBehaviour {
    //enemy healthbar
    private float health;
    private float maxhealth;
    private bool playerRight = false;
    //private int size;
    Vector3 theScale;
    Vector3 originalsize;
    // Use this for initialization
    void Start () {
        health = this.transform.parent.GetComponent<enemyScript>().health;
        playerRight = this.transform.parent.GetComponent<enemyScript>().playerRight;
        maxhealth = health;
        theScale = transform.localScale;
        originalsize = transform.localScale;

        //Vector3 theScale2 = transform.localScale;
        //theScale2.x = theScale2.x * -1;
        //transform.localScale = theScale2;
    }
	
	// Update is called once per frame
	void Update () {
        health = this.transform.parent.GetComponent<enemyScript>().health;
  
        theScale.x = (originalsize.x / maxhealth) * health;
        transform.localScale = theScale;
        playerRight = this.transform.parent.GetComponent<enemyScript>().playerRight;

        Vector3 theScale2 = transform.localScale;

        if (playerRight == true)
        {
            if (theScale2.x > 0)
            {
                theScale2.x = theScale2.x * -1;
                transform.localScale = theScale2;
            }
          
        }

    }

}
