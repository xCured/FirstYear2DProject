using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class dontdestroy : MonoBehaviour {

    // Use this for initialization
void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Scene currentScene = SceneManager.GetActiveScene();
        string SceneName = currentScene.name;
        if (SceneName == "menuscreen")
        {
            Destroy(this.gameObject);
        }


    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);


    }
}
