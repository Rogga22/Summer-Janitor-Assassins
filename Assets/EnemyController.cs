using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject player;

    int state = 0; //0 is idle, 1 is windup, 2 is attacking, 3 is staggered
    public int health = 3;
    float windup = 1;
    float backswing = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (state == 1 && windup > 0)
        {
            windup -= Time.deltaTime;
        }
        else if (state == 1 && windup <= 0)
        {
            state = 2;
            backswing = 0.3f;
        }

        if (state == 2)
        {
            
        }
		
	}

    void hit ()
    {
        //Lowers health when hit, does more damage when staggered
        if(state == 3)
        {
            health -= 3;
        }
        else
        {
            health -= 1;
        }
    }

    void typeSet (int type)
    {
        //sets the type of enemy, to be filled out
    }
}
