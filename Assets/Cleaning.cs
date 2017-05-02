using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour {

    public PlayerController play;
    bool iscleaning;
    bool overbody;
    bool carrying;
    bool nearhide;
    GameObject body;
    public LayerMask enemyHitbox;
    GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        iscleaning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) //Does an attack, put attack animation in here
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2, enemyHitbox);
            Debug.Log("attack");
            foreach (Collider2D c in colliders)
            {
                EnemyController enemy = c.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.hit();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.V) && !carrying) //activates cleaning, needs animation
        {
                play.movespd = play.slowspd;
                iscleaning = true;
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            play.movespd = play.maxspd;
            iscleaning = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && !iscleaning && !carrying)
        {
            carrying = true;
        }

        if (carrying)
        {
            play.onfloor = false;
            body.transform.position = play.transform.position;
            play.movespd = play.maxspd / 2;
            if (Input.GetKeyDown(KeyCode.D) && !nearhide)
            {
                body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y - .8f, body.transform.position.z);
                carrying = false;
                overbody = false;
                play.onfloor = true;
                play.movespd = play.maxspd;
            }
            if (Input.GetKeyDown(KeyCode.D) && nearhide)
            {
                Destroy(body);
                carrying = false;
                overbody = false;
                play.onfloor = true;
                play.movespd = play.maxspd;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blood" && iscleaning)
        {
            other.transform.localScale -= new Vector3(.01f, .01f, 0);
            if (other.transform.localScale.x <= 0.1f)
            {
                Destroy(other.gameObject);
                manager.messiness--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trash" && !carrying)
        {
            overbody = true;
            body = other.gameObject;
        }
        if (other.gameObject.tag == "Disposal")
        {
            nearhide = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trash")
        {
            overbody = false;
        }
        if (other.gameObject.tag == "Disposal")
        {
            nearhide = false;
        }
    }
}

        if (Input.GetKeyDown(KeyCode.X) && !play.parrying) //Does an attack, put attack animation in here
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2, enemyHitbox);
            Debug.Log("attack");
            foreach (Collider2D c in colliders)
            {
                EnemyController enemy = c.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.hit();
                }
            }