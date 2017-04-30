using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour {

    public EnemyController parent;
    public bool playerColliding = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(parent.state);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(parent.state);
        /* if (parent.state == 2 && !(parent.alreadyHit) && col.gameObject.tag == "Player")
         {
             bool parried = parent.player.attacked();
             if (parried)
             {
                 parent.state = 3;
                 parent.recovery = 0.75f;
                 GetComponent<SpriteRenderer>().color = Color.blue;
                 Debug.Log("Parried");
             }
             else
             {
                 parent.alreadyHit = true;
                 Debug.Log("Hit");
             }
         }*/
        if (col.gameObject.tag == "Player")
        {
            playerColliding = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerColliding = false;
        }
    }
}
