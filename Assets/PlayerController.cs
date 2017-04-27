using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D RB;
    public float movespd;
    public float jmp;
    bool onfloor;

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = RB.velocity;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (move.x > 0)
                move.x = 0;
            move.x = Mathf.Lerp(move.x, -movespd, 0.1f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (move.x < 0)
                move.x = 0;
            move.x = Mathf.Lerp(move.x, movespd, 0.1f);
        }
        else
            move.x = Mathf.Lerp(move.x, 0, 0.1f);
        if (Input.GetKeyDown(KeyCode.Z) && onfloor == true)
        {
            move.y = jmp;
            onfloor = false;
        }
        RB.velocity = move;
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            onfloor = true;
        }
    }
}


