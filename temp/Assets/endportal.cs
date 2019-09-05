using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endportal : MonoBehaviour {

    // Use this for initialization

    public GameObject bluepedestal;
    public GameObject greenpedestal;
    public GameObject whitepedestal;
    public GameObject redpedestal;

    private bool blueactive = false;
    private bool greenactive = false;
    private bool whiteactive = false;
    private bool redactive = false;

    void Start () {
        //bluepedestal = GameObject.Find("PedestalBlue");
        //greenpedestal = GameObject.Find("PedestalGreen");
        //whitepedestal = GameObject.Find("PedestalWhite");
        //redpedestal = GameObject.Find("PedestalRed");

        
    }
    // && greenpedestal.GetComponent<PedestalScript>().active == true && whitepedestal.GetComponent<PedestalScript>().active == true && redpedestal.GetComponent<PedestalScript>().active == true
    // Update is called once per frame

    void Update () {
        blueactive = bluepedestal.GetComponent<PedestalScript>().active;
        greenactive = greenpedestal.GetComponent<PedestalScript>().active;
        whiteactive = whitepedestal.GetComponent<PedestalScript>().active;
        redactive = redpedestal.GetComponent<PedestalScript>().active;

    

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (blueactive == true && greenactive == true && whiteactive == true && redactive == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Application.LoadLevel("EndScene");
            }
        }
    }


}
