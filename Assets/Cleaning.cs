using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour {

    public PlayerController play;
    bool iscleaning;

    void Start()
    {
        iscleaning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            play.movespd = play.slowspd;
            iscleaning = true;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            play.movespd = play.maxspd;
            iscleaning = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blood" && iscleaning)
        {
            other.transform.localScale -= new Vector3(.01f, .01f, 0);
            if(other.transform.localScale.x <= 0.1f)
                Destroy(other.gameObject);
        }
    }
}
