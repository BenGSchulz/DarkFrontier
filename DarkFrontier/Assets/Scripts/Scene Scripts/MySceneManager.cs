using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour {

    private GameObject gameModel;
    private GameData data;

    public GameObject player;
    private PlayerMovement pm;
    private Gun gun;


    public Color ambientLightColor;
    public GameObject fastEnemy;
    public GameObject strongEnemy;
    public List<GameObject> enemies;
    public int enemyCount;
    public int maxEnemyCount;

    public GameObject ammo;
    public List<GameObject> ammoDrops;
    public int ammoDropCount;
    public int ammoDropMax;

    public int enemiesKilled;
    public int enemyGoal;

    public bool paused;
    public GameObject pauseUI;

    public Text killCount;

	// Use this for initialization
	void Start () {
        RenderSettings.ambientLight = ambientLightColor;

        gameModel = GameObject.FindGameObjectWithTag("GameModel");
        data = gameModel.GetComponent<GameData>();

        player = GameObject.FindGameObjectWithTag("Player");

        pm = player.GetComponent<PlayerMovement>();
        pm.startPos = data.playerPos;

        gun = player.GetComponent<Gun>();
        gun.bulletCount = data.bulletCount;
        gun.bulletStock = data.bulletStock;

        ammoDrops = new List<GameObject>();

        enemies = new List<GameObject>();
        enemyCount = 0;
        enemiesKilled = data.killCount;

        paused = false;
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

        killCount.text = "x " + enemiesKilled.ToString();

        while (enemyCount < maxEnemyCount) {
            if (enemyCount % 2 == 0) {
                enemies.Add((GameObject) Instantiate(fastEnemy));
                enemyCount++;
            } else {
                enemies.Add((GameObject) Instantiate(strongEnemy));
                enemyCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                paused = false;
                pauseUI.GetComponent<Canvas>().enabled = false;
                Cursor.visible = false;
            } else {
                paused = true;
                pauseUI.GetComponent<Canvas>().enabled = true;
                Cursor.visible = true;
            }
        }
    }

    public void ammoDrop (Vector3 pos) {
        if (ammoDropCount < ammoDropMax) {
            ammoDrops.Add((GameObject) Instantiate(ammo));
            ammoDropCount++;
            ammoDrops[ammoDropCount - 1].transform.position = pos;
        }
    }

    public void saveGame() {
        data.bulletCount = gun.bulletCount;
        data.bulletStock = gun.bulletStock;
        data.killCount = enemiesKilled;
        data.playerPos = player.transform.position;
  
        gameModel.GetComponent<GameModel>().onSaveClick();
    }

    public void loadGame() {
        gameModel.GetComponent<GameModel>().onLoadClick();
    }
}
