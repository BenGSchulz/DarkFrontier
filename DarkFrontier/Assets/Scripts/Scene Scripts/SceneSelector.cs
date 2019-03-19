using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour {

    private GameObject gameModel;
    private GameData data;

	// Use this for initialization
	void Start () {
        gameModel = GameObject.FindGameObjectWithTag("GameModel");
        if (gameModel != null) {
            data = gameModel.GetComponent<GameData>();
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void MainScene() {
        Cursor.visible = false;
        data.playerPos = new Vector3(2f, 7f, 0f);
        data.bulletCount = 6;
        data.bulletStock = 6;
        data.killCount = 0;

        SceneManager.LoadScene("Main");
    }

    public void Title() {
        Cursor.visible = true;
        SceneManager.LoadScene("Title");

    }

    public void Credits() {
        Cursor.visible = true;
        SceneManager.LoadScene("Credits");

    }

    public void Guide() {
        Cursor.visible = true;
        SceneManager.LoadScene("Guide");

    }

    public void Dead() {
        Cursor.visible = true;
        SceneManager.LoadScene("Dead");
    }

    public void Win() {
        Cursor.visible = true;
        SceneManager.LoadScene("Win");

    }

    public void loadStart() {
        Cursor.visible = false;
        gameModel.GetComponent<GameModel>().onLoadClick();
    }

}
