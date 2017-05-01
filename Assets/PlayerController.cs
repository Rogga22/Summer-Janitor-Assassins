using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D RB;
    public float maxspd;
    public float movespd;
    public float slowspd;
    public float jmp;
    bool onfloor;

    float parryCooldown = 0;
    float parryTimer = 0;
    bool parrying = false;

    bool lose = false;

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        movespd = maxspd;
        slowspd = maxspd * .2f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!lose)
        {
            Vector2 move = RB.velocity;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (move.x > 0)
                    move.x = 0;
                move.x = Mathf.Lerp(move.x, -movespd, 0.1f);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (move.x < 0)
                    move.x = 0;
                move.x = Mathf.Lerp(move.x, movespd, 0.1f);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
                move.x = Mathf.Lerp(move.x, 0, 0.1f);
            if (Input.GetKeyDown(KeyCode.Z) && onfloor == true)
            {
                move.y = jmp;
                onfloor = false;
            }
            RB.velocity = move;

            if (Input.GetKeyDown(KeyCode.C) && parryCooldown <= 0)
            {
                parrying = true;
                parryTimer = 0.5f;
                parryCooldown = 1.5f;
                GetComponent<SpriteRenderer>().color = Color.green; //replace with sprites
            }

            if (parryCooldown > 0)
            {
                parryCooldown -= Time.deltaTime;
            }
            if (parryTimer > 0)
            {
                parryTimer -= Time.deltaTime;
            }
            else if (parryTimer <= 0)
            {
                parrying = false;
                GetComponent<SpriteRenderer>().color = Color.white; //replace with sprites
            }
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            onfloor = true;
        }
    }

    public bool attacked()
    {
        if(parrying)
        {
            parrying = false;
            GetComponent<SpriteRenderer>().color = Color.white; //replace with sprites
            return true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red; //replace with death sprite
            lose = true;
            return false;
        }

    }
}


