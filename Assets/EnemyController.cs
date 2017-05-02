using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody2D RB;
    public PlayerController player;
    public EnemyHurtbox attackBox;
    public GameManager manager;

    public bool left;
    public int state = 0; //0 is idle, 1 is windup, 2 is attacking, 3 is staggered, 4 is death animation
    public int health = 3;
    float windup = 0.5f;
    float backswing = 0.3f;
    public float recovery = 0.75f;
    public bool alreadyHit = false;
    int totalMotion = 0;
    public int type = 0;

    public GameObject blood;

    public AudioClip struck;
    public AudioClip slain;
    public AudioClip blocked;
    private AudioSource sound;
    Vector3 deadPos;

    public Sprite idleRight;
    public Sprite idleLeft;
    public Sprite windupRight;
    public Sprite windupLeft;
    public Sprite attackRight;
    public Sprite attackLeft;
    public Sprite staggerRight;
    public Sprite staggerLeft;
    public Sprite dead;

	// Use this for initialization
	void Start () {
        if (type == 2)
        {
            health = 15;
        }
        RB = GetComponent<Rigidbody2D>();
        manager = GameObject.FindObjectOfType<GameManager>();
        player = GameObject.FindObjectOfType<PlayerController>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        if (state == 0 && recovery <= 0)
        {
            Vector3 where = transform.position;

            //Vector2 move = RB.velocity;
            if (type == 2)
            {
                int freeWill = (int)Random.Range(0f, 500f);
                if (freeWill >= 482)
                {
                    state = 1;
                    windup = 0.6f;
                    if (!left)
                    {
                        GetComponent<SpriteRenderer>().sprite = windupRight;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = windupLeft;
                    }
                }
                else if (freeWill >= 477 && totalMotion < 4)
                {
                    //move.x = Mathf.Lerp(move.x, 50, 0.1f);
                    //Debug.Log("Moving left");
                    where += new Vector3(0.2f, 0, 0);
                    totalMotion++;
                    recovery = 0.3f;
                }
                else if (freeWill >= 474 && totalMotion > -4)
                {
                    //move.x = Mathf.Lerp(move.x, -50, 0.1f);
                    //Debug.Log("Moving right");
                    where += new Vector3(-0.2f, 0, 0);
                    totalMotion--;
                    recovery = 0.3f;
                }
            }
            else
            {
                int freeWill = (int)Random.Range(0f, 500f);
                if (freeWill >= 492)
                {
                    state = 1;
                    windup = 0.6f;
                    if (!left)
                    {
                        GetComponent<SpriteRenderer>().sprite = windupRight;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = windupLeft;
                    }
                }
                else if (freeWill >= 487 && totalMotion < 4)
                {
                    //move.x = Mathf.Lerp(move.x, 50, 0.1f);
                    //Debug.Log("Moving left");
                    where += new Vector3(0.2f, 0, 0);
                    totalMotion++;
                    recovery = 0.3f;
                }
                else if (freeWill >= 484 && totalMotion > -4)
                {
                    //move.x = Mathf.Lerp(move.x, -50, 0.1f);
                    //Debug.Log("Moving right");
                    where += new Vector3(-0.2f, 0, 0);
                    totalMotion--;
                    recovery = 0.3f;
                }
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
            if (!left)
            {
                GetComponent<SpriteRenderer>().sprite = attackRight;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = attackLeft;
            }
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
                    if (!left)
                    {
                        GetComponent<SpriteRenderer>().sprite = staggerRight;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = staggerLeft;
                    }
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
            if (!left)
            {
                GetComponent<SpriteRenderer>().sprite = idleRight;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = idleLeft;
            }
            recovery = 1f;
        }

        if (state == 3 && recovery > 0)
        {
            recovery -= Time.deltaTime;
        }
        else if (state == 3 && recovery <= 0)
        {
            state = 0;
            if (!left)
            {
                GetComponent<SpriteRenderer>().sprite = idleRight;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = idleLeft;
            }
            recovery = 1f;
        }

        /*if (state == 4 && recovery > 0)
        {
            recovery -= Time.deltaTime;
        }
        else if (state == 4 && recovery <= 0)
        {
            Destroy(gameObject);
        }*/
		
	}

    public void hit()
    {
        bool successful = false;
        if (state != 4 && !player.lose)
        {
            //Lowers health when hit, does more damage when staggered
            if (state == 3)
            {
                health -= 3;
                successful = true;
            }
            else
            {
                if (type == 0)
                {
                    health -= 1; //Have them go into a very short flinch animation or something
                    successful = true;
                }
                //Have them go into a very short blocking animation
            }
            if (health <= 0)
            {
                Instantiate(blood, new Vector3(transform.position.x + (Random.Range(-1f, 1)), transform.position.y, transform.position.z), Quaternion.identity);
                Instantiate(blood, new Vector3(transform.position.x + (Random.Range(-1f, 1)), transform.position.y, transform.position.z), Quaternion.identity);
                Instantiate(blood, new Vector3(transform.position.x + (Random.Range(-1f, 1)), transform.position.y, transform.position.z), Quaternion.identity);
                manager.messiness += 3;
                state = 4;
                GetComponent<SpriteRenderer>().sprite = dead;
                deadPos = transform.position;
                deadPos.y -= 1f;
                transform.position = deadPos;
                transform.gameObject.tag = "Trash";
                //CHANGE SPRITE TO DYING ANIMATION HERE
                sound.PlayOneShot(slain);
            }
            else if (successful)
            {
                sound.PlayOneShot(struck);
            }
            else
            {
                sound.PlayOneShot(blocked);
            }
        }
    }

    void typeSet (int t)
    {
        type = t;
    }

}
