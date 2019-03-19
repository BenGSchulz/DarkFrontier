using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("Title");
        SceneManager.LoadScene("Credits");
        SceneManager.LoadScene("Guide");
        SceneManager.LoadScene("Main");

        Title();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Main() {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
    }

    public void Title() {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Title"));
    }
}
