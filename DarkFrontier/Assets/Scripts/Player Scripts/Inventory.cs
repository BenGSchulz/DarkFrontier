using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private enum HeldItem {Lantern, Gun, Torch};
    private enum PassiveItem {};

    private HeldItem currentHeld;
    private PassiveItem currentPassive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    HeldItem getHeld() {
        return currentHeld;
    } 

    void setHeld(HeldItem newHeld) {
        currentHeld = newHeld;
    }

    PassiveItem getPassive() {
        return currentPassive;
    }

    void setPassive(PassiveItem newPassive) {
        currentPassive = newPassive;
    }

}
