using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager manager;

    Rigidbody2D RB;
    public float maxspd;
    public float movespd;
    public float slowspd;
    public float jmp;
    public bool onfloor;
    public bool faceRight = true;
    public bool actions = false;

    float parryCooldown = 0;
    float parryTimer = 0;
    public bool parrying = false;

    public bool lose = false;

    public AudioClip parrySound;
    public AudioClip deathSound;
    private AudioSource sound;

    public Sprite idleLeft;
    public Sprite idleRight;
    public Sprite carryingLeft;
    public Sprite carryingRight;
    public Sprite dead;
    public Sprite blockLeft;
    public Sprite blockRight;
    public Sprite attackLeft;
    public Sprite attackRight;
    public Sprite walkLeft;
    public Sprite walkRight;
    public Sprite cleanLeft;
    public Sprite cleanRight;

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        manager = GameObject.FindObjectOfType<GameManager>();
        movespd = maxspd;
        slowspd = maxspd * .2f;
        sound = GetComponent<AudioSource>();
        manager.setOffset(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lose)
        {
            Vector2 move = RB.velocity;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                faceRight = false;
                if(!actions)
                    GetComponent<SpriteRenderer>().sprite = walkLeft;
                if (move.x > 0)
                    move.x = 0;
                move.x = Mathf.Lerp(move.x, -movespd, 0.1f);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                faceRight = true;
                if (!actions)
                    GetComponent<SpriteRenderer>().sprite = walkRight;
                if (move.x < 0)
                    move.x = 0;
                move.x = Mathf.Lerp(move.x, movespd, 0.1f);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                move.x = Mathf.Lerp(move.x, 0, 0.1f);
                if(faceRight && !actions)
                {
                    GetComponent<SpriteRenderer>().sprite = idleRight;
                }
                else if(!actions)
                {
                    GetComponent<SpriteRenderer>().sprite = idleLeft;
                }
            }
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
                if (faceRight)
                {
                    GetComponent<SpriteRenderer>().sprite = blockRight;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = blockLeft;
                }
            }

            if (parryCooldown > 0)
            {
                parryCooldown -= Time.deltaTime;
            }
            if (parryTimer > 0)
            {
                parryTimer -= Time.deltaTime;
            }
            else if (parryTimer <= 0 && parrying)
            {
                parrying = false;
                if (faceRight)
                {
                    GetComponent<SpriteRenderer>().sprite = idleRight;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = idleLeft;
                }
            }
            manager.move(transform.position);
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
        if (!lose)
        {
            if (parrying)
            {
                parrying = false;
                if (faceRight)
                {
                    GetComponent<SpriteRenderer>().sprite = idleRight;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = idleLeft;
                }
                sound.PlayOneShot(parrySound);
                return true;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = dead;
                lose = true;
                sound.PlayOneShot(deathSound);
                return false;
            }
        }
        return false;
    }
}


