using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

    private GameObject Player;
    private Gun playerGun;

    private AudioSource aud;
    public AudioClip pickup;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerGun = Player.GetComponent<Gun>();
        aud = Player.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == Player) {
            playerGun.bulletStock += 3;
            Debug.Log("Ammo Collected!");
            aud.clip = pickup;
            aud.Play();
            Destroy(gameObject);
        }
    }
}
