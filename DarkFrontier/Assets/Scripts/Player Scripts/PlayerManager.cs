using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private enum HeldItem { Lantern, Gun, Torch };
    private enum PassiveItem { };


    private List<HeldItem> inventory;
    private HeldItem currentHeld;
    private PassiveItem currentPassive;

    private int heldIndex;

    GameObject lantern;
    Light lanternLight;
    GameObject torch;
    Light torchLight;

	// Use this for initialization
	void Start () {
        lantern = GameObject.FindGameObjectWithTag("Lantern");
        torch = GameObject.FindGameObjectWithTag("Torch");

        lanternLight = lantern.GetComponent<Light>();
        torchLight = torch.GetComponent<Light>();

        inventory = new List<HeldItem>();

        inventory.Add(HeldItem.Lantern);
        inventory.Add(HeldItem.Gun);
        inventory.Add(HeldItem.Torch);

        heldIndex = 0;

        enableLantern();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("SwapHeld")) {
            if (heldIndex < inventory.Count-1) {
                heldIndex++;
            } else {
                heldIndex = 0;
            }
            currentHeld = inventory[heldIndex];
        }

        switch (currentHeld) {
            case HeldItem.Lantern:
                //Debug.Log("Lantern Held");
                enableLantern();
                break;
            case HeldItem.Gun:
                //Debug.Log("Gun Held");
                disableLights();
                break;
            case HeldItem.Torch:
                //Debug.Log("Torch held");
                enableTorch();
                break;
            default:
                break;
        }
    }

    void enableLantern() {
        lanternLight.enabled = true;
        disableTorch();
    }

    void disableLantern() {
        lanternLight.enabled = false;
    }

    void enableTorch() {
        torchLight.enabled = true;
        disableLantern();
    }

    void disableTorch() {
        torchLight.enabled = false;
    }

    void disableLights() {
        disableLantern();
        disableTorch();
    }
}
