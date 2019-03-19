using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// For serialization
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GameModel : MonoBehaviour {

    private static GameModel singleton;

    //A property to access the instance
    public static GameModel Instance { get { return singleton; } }

    //public GameObject player;
    //public Gun gun;
    public GameData data;
    public string saveFileName;

    //Only works for single threaded apps
    void Awake() {
        //set up singleton
        if (singleton == null) {
            Debug.Log("Created Singleton of " + gameObject.ToString());
            singleton = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Debug.Log("There was already a game model");
            Destroy(gameObject);
        }

        data = this.gameObject.GetComponent<GameData>();
        //Debug.Log(Application.persistentDataPath);
    }

    public void onSaveClick() {
        SaveGame saveGame = new SaveGame();
        saveGameModel(saveGame, saveFileName);
    }

    public void onLoadClick() {
        if (loadGame(saveFileName)) {
            SceneManager.LoadScene("Main");
        }
    }

    public void saveGameModel(SaveGame save, string filename) {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Writing file to: " + Application.persistentDataPath + "/" + filename + ".dat");
        FileStream fs = File.OpenWrite(Application.persistentDataPath + "/" + filename + ".dat");

        
        save.StoreData(this);

        bf.Serialize(fs, save);

        fs.Close();
    }

    public bool loadGame(string filename) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs;

        try {
            fs = File.OpenRead(Application.persistentDataPath + "/" + filename + ".dat");
        } catch (FileNotFoundException e) {
            Debug.Log("File was not created yet (no save)! filename = " + e.FileName);
            return false;
        }
        

        SaveGame saveGame = (SaveGame)bf.Deserialize(fs);

        saveGame.LoadData(this);

        fs.Close();
        return true;
    }
}
