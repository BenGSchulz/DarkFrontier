using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private GameObject player;
    private Vector3 posVec;
    //private TargetJoint2D targetJoint;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        posVec = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = posVec;

        //targetJoint = GetComponent<TargetJoint2D>();
    }

    // Update is called once per frame
    void Update() {

        //targetJoint.target = player.transform.position;

        posVec = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = posVec;
    }
}
