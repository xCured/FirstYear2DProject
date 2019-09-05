using UnityEngine;
using System.Collections;

public class PedestalScript : MonoBehaviour {

    // Use this for initialization
    public bool blue = false;
    public bool red = false;
    public bool white = false;
    public bool green = false;

    public bool active=false;

    private bool unlocked = false;

    // private bool unlocked = false;
    private GameObject player;
    void Start () {
        player = GameObject.Find("animPlayer");
    }
	
	// Update is called once per frame
	void Update () {
	    if (blue == true)
        {
            unlocked = player.GetComponent<PlayerManager>().bluerod;
        }
        if (green == true)
        {
            unlocked = player.GetComponent<PlayerManager>().greenrod;
        }
        if (red == true)
        {
            unlocked = player.GetComponent<PlayerManager>().redrod;
        }
        if (white == true)
        {
            unlocked = player.GetComponent<PlayerManager>().whiterod;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (Input.GetKeyDown(KeyCode.C) && blue == true && unlocked == true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pedestal/PedestalBlueA") as Sprite;
                active = true;
            }

            if (Input.GetKeyDown(KeyCode.C) && green == true && unlocked == true)
            {
           
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pedestal/PedestalGreenA") as Sprite;
                active = true;
            }

            if (Input.GetKeyDown(KeyCode.C) && white == true && unlocked == true)
            {
    
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pedestal/PedestalWhiteA") as Sprite;
                active = true;
            }

            if (Input.GetKeyDown(KeyCode.C) && red == true && unlocked == true)
            {
         
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pedestal/PedestalRedA") as Sprite;
                active = true;
            }
        }
    }
}
