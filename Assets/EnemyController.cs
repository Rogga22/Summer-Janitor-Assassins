using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody2D RB;
    public PlayerController player;
    public EnemyHurtbox attackBox;

    public int state = 0; //0 is idle, 1 is windup, 2 is attacking, 3 is staggered
    public int health = 3;
    float windup = 0.5f;
    float backswing = 0.3f;
    public float recovery = 0.75f;
    public bool alreadyHit = false;
    int totalMotion = 0;
    public int type = 0;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (state == 0 && recovery <= 0)
        {
            Vector3 where = transform.position;

            //Vector2 move = RB.velocity;
            int freeWill = (int)Random.Range(0f, 500f);
            if (freeWill >= 496)
            {
                state = 1;
                windup = 0.6f;
                GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            else if (freeWill >= 491 && totalMotion < 4)
            {
                //move.x = Mathf.Lerp(move.x, 50, 0.1f);
                //Debug.Log("Moving left");
                where += new Vector3(0.2f, 0, 0);
                totalMotion++;
                recovery = 0.3f;
            }
            else if (freeWill >= 488 && totalMotion > -4)
            {
                //move.x = Mathf.Lerp(move.x, -50, 0.1f);
                //Debug.Log("Moving right");
                where += new Vector3(-0.2f, 0, 0);
                totalMotion--;
                recovery = 0.3f;
            }
            transform.position = where;
        }
        else if (state == 0 && recovery > 0)
        {
            recovery -= Time.deltaTime;
        }

        if (state == 1 && windup > 0)
        {
            windup -= Time.deltaTime;
        }
        else if (state == 1 && windup <= 0)
        {
            state = 2;
            backswing = 0.3f;
            alreadyHit = false;
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (state == 2 && backswing > 0)
        {
            if (!alreadyHit && attackBox.playerColliding)
            {
                bool parried = player.attacked();
                if (parried)
                {
                    state = 3;
                    recovery = 0.75f;
                    GetComponent<SpriteRenderer>().color = Color.blue;
                    //Debug.Log("Parried");
                }
                else
                {
                    alreadyHit = true;
                }
            }
            backswing -= Time.deltaTime;
        }
        else if (state == 2 && backswing <= 0)
        {
            state = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
            recovery = 1f;
        }

        if (state == 3 && recovery > 0)
        {
            recovery -= Time.deltaTime;
        }
        else if (state == 3 && recovery <= 0)
        {
            state = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
            recovery = 1f;
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
            if (type != 1)
            {
                health -= 1;
            }
        }
    }

    void typeSet (int t)
    {
        type = t;
    }

}
