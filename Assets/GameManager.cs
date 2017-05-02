using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int messiness = 0;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setOffset(Vector3 playerPos)
    {
        offset = transform.position - playerPos;
    }

    public void move(Vector3 moving)
    {
        transform.position = moving + offset;
    }
}
