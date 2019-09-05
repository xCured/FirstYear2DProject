using UnityEngine;
using System.Collections;

public class OrbScript : MonoBehaviour {
    private int orbtype = 1;
    private bool firelevel;
    private bool waterlevel;
    private bool earthlevel;
    private bool airlevel;

    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("animPlayer");
        firelevel = player.GetComponent<PlayerManager>().fireUnlocked;
    waterlevel = player.GetComponent<PlayerManager>().waterUnlocked;
    earthlevel = player. GetComponent<PlayerManager>().earthUnlocked;
    airlevel = player.GetComponent<PlayerManager>().airUnlocked;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {   
        player.GetComponent<PlayerManager>().maxelement = orbtype;
        Destroy(gameObject);
    }

    //test = GetComponent<PlayerManager>().health;
    // Update is called once per frame
    void Update () {
        firelevel = player.GetComponent<PlayerManager>().fireUnlocked;
        waterlevel = player.GetComponent<PlayerManager>().waterUnlocked;
        earthlevel = player.GetComponent<PlayerManager>().earthUnlocked;
        airlevel = player.GetComponent<PlayerManager>().airUnlocked;

       
        if (firelevel == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("orb/FireOrb") as Sprite;
           
            orbtype = 4;
        }
        else if (airlevel == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("orb/AirOrb") as Sprite;
           
            orbtype = 3;
        }
        else if (earthlevel == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("orb/EarthOrb") as Sprite;
            orbtype = 2;
        }
        else if (waterlevel == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("orb/WaterOrb") as Sprite;
            orbtype = 1;
        }

    }

    
}
