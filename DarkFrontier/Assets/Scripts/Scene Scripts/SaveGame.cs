using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;


[Serializable]
public class SaveGame : ISerializable {
    public int bulletCount { get; set; }
    public int bulletStock { get; set; }
    public int killCount { get; set; }
    public bool saved { get; set; }
    public Vector3 playerPos;

    public SaveGame() { }

    public SaveGame(SerializationInfo info, StreamingContext context) {
        bulletCount = info.GetInt32("bulletCount");
        bulletStock = info.GetInt32("bulletStock");
        killCount = info.GetInt32("killCount");
        saved = info.GetBoolean("saved");
        playerPos = new Vector3(info.GetSingle("posx"), info.GetSingle("posy"), info.GetSingle("posz"));
    }

    public void StoreData(GameModel model) {
        bulletCount = model.data.bulletCount;
        bulletStock = model.data.bulletStock;
        killCount = model.data.killCount;
        playerPos = model.data.playerPos;
        saved = true;
    }

    public void LoadData(GameModel model) {
        model.data.bulletCount = bulletCount;
        model.data.bulletStock = bulletStock;
        model.data.killCount = killCount;
        model.data.playerPos = playerPos;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        info.AddValue("bulletCount", bulletCount);
        info.AddValue("bulletStock", bulletStock);
        info.AddValue("killCount", killCount);
        info.AddValue("saved", saved);
        info.AddValue("posx", playerPos.x);
        info.AddValue("posy", playerPos.y);
        info.AddValue("posz", playerPos.z);
    }

}