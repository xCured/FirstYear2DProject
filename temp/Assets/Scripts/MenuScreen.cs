using UnityEngine;
using System.Collections;

public class MenuScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startGame()
    {
        Application.LoadLevel("turtorial");
    }

    public void Instructions()
    {
        Application.LoadLevel("Instructions");
    }

    public void mainmenu()
    {
        Application.LoadLevel("menuscreen");
    }
}
