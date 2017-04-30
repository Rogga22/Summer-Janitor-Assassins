using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour {

    public PlayerController play;
    bool iscleaning;
    int enemiesInRange = 0; //Detects how many enemies are within the hurtbox
    public LayerMask enemyHitbox;

    void Start()
    {
        iscleaning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) //Does an attack if enemies are in range, and cleans if not
        {
            if (enemiesInRange > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2, enemyHitbox);
                Debug.Log("attack");
                foreach (Collider2D c in colliders)
                {
                    EnemyController enemy = c.gameObject.GetComponent<EnemyController>();
                    if (enemy.hit())
                    {
                        enemiesInRange--;
                    }
                }
            }
            else
            {
                play.movespd = play.slowspd;
                iscleaning = true;
            }
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("in range");
            enemiesInRange += 1;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemiesInRange -= 1;
        }
    }
}
