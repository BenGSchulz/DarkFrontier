using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private GameObject gameModel;

    private GameData data;

	// Use this for initialization
	void Start () {
        gameModel = GameObject.FindGameObjectWithTag("GameModel");
        data = gameModel.GetComponent<GameData>();

        Debug.Log("The Player Pos from game data is " + data.playerPos.ToString() + "\nThe saved value is " + data.saved);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
