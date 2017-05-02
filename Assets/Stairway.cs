using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairway : MonoBehaviour {

    public GameObject exit;
    GameObject player;
    bool inrange;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && inrange) {
            player.transform.position = exit.transform.position;
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == player)
            inrange = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == player)
            inrange = false;
    }
}
