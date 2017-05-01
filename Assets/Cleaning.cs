using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour {

    public PlayerController play;
    bool iscleaning;
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

        if(Input.GetKeyDown(KeyCode.V)) //activates cleaning, needs animation
        {
                play.movespd = play.slowspd;
                iscleaning = true;
        }

        if (Input.GetKeyUp(KeyCode.V))
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
            if (other.transform.localScale.x <= 0.1f)
            {
                Destroy(other.gameObject);
                manager.messiness--;
            }
        }
    }
}
