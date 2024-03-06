using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.U2D;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedChange;
    [SerializeField] Rigidbody2D rb;
    public Vector2 acceleration;
    [SerializeField] float angle;
    [SerializeField] float angleChange;
    [SerializeField] GameObject bullet;
    [SerializeField] Text scoreText;
    [SerializeField] Text over;
    [SerializeField] Text points;
    private bool forward, brake, right, left;
    public int friction;
    public int Max;
    public bool front = false;
    public bool backward = false;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spirteRenderer2;
    public SpriteRenderer spirteRenderer3;
    public SpriteRenderer spirteRenderer4;
    public SpriteRenderer ship;
    public Animator anim;
    public float MaxTime = .5f;
    public float TimeLeft;
    public bool gameover = false;
    [SerializeField] int lives = 3;
    public int score;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 OgPos;
    public GameObject player;
    public GameObject BigAst;
    public GameObject MediumAst;
    public GameObject SmallAst;
    public GameObject rearbooster;
    public GameObject frontboost;
    public GameObject frontboost1;
    public bool death = true;
    public GameObject asteroidLarge;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public float x, y;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning());
        x = -11.87f;
        y = -2.35f;
        position = new Vector3(x, y);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        score = 0;
        over.text = "";
        scoreText.text = "Lives:";
        points.text = "Points: " + score;
        OgPos = transform.position;
        TimeLeft = 0f;
        if (gameObject.tag.Equals("boost")||gameObject.tag.Equals("front boost"))
        {
            this.spriteRenderer1.enabled = false;
            this.spirteRenderer2.enabled = false;
            this.spirteRenderer3.enabled = false;
        }
        rb = GetComponent<Rigidbody2D>();
        Max = 600;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (lives == 2)
        {
            Destroy(GameObject.FindWithTag("Life1"));
        }
        if (lives == 1)
        {
            Destroy(GameObject.FindWithTag("Life2"));
        }
        if (lives == 0)
        {
            gameover = true;
            Destroy(GameObject.FindWithTag("Life3"));
            this.ship.enabled = false;
            this.spriteRenderer1.enabled = false;
            this.spirteRenderer2.enabled = false;
            this.spirteRenderer3.enabled = false;
            this.spirteRenderer4.enabled = false;
            over.text = "Game Over! Click R to Restart";
        }
        if (TimeLeft > 0f)
        {
            Debug.Log("Time");
            TimeLeft -= Time.deltaTime*2;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (gameover==false)
            {
                if (TimeLeft <= 0)
                {
                    Debug.Log("shoot");
                    GetComponent<AudioSource>().Play();
                    this.spirteRenderer4.enabled = true;
                    GameObject b = Instantiate(bullet, transform.position + transform.right * 1.5f, transform.rotation);
                    b.GetComponent<Rigidbody2D>().velocity = b.transform.right * 8f;
                    TimeLeft = MaxTime;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameover == false)
            {
                forward = true;
                if (death)
                {
                    this.spriteRenderer1.enabled = true;
                    anim.Play("Booster Animation");
                }
            }
            /*front = true;
            backward = false;*/
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(gameover==false)
            {
                brake = true;
                StartCoroutine(Spawning());
                if (death)
                {
                    this.spirteRenderer2.enabled = true;
                    this.spirteRenderer3.enabled = true;
                }
            }
            /*backward = true;
            front = false;*/
        }
        if (Input.GetKeyDown(KeyCode.A))
            left = true;
        if (Input.GetKeyDown(KeyCode.D))
            right = true;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.spirteRenderer4.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
            this.spriteRenderer1.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            brake = false;
            this.spirteRenderer2.enabled = false;
            this.spirteRenderer3.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
            left = false;
        if (Input.GetKeyUp(KeyCode.D))
            right = false;

        //Ship teleportation
        Vector2 newPos = transform.position;
        if (transform.position.y > 5.55f)
        {
            newPos.y = -5.55f;
        }
        if (transform.position.y < -5.55f)
        {
            newPos.y = 5.55f;
        }
        if (transform.position.x > 11.26f)
        {
            newPos.x = -11.26f;
        }
        if (transform.position.x < -11.26f)
        {
            newPos.x = 11.26f;
        }
        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        /*if (gameObject.tag.Equals("player"))
        {*/
            if (left)
            {
                angle += angleChange * Time.deltaTime;
            }
            if (right)
            {
                angle -= angleChange * Time.deltaTime;
            }
        /*}*/
        if (forward)
        {
            if (front == false && backward == true)
            {
                speed = 3;
            }
            if (speed < Max)
            {
                speed += speedChange * Time.deltaTime;
            }
            rb.velocity += new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * speed * Time.deltaTime, Mathf.Sin(Mathf.Deg2Rad * angle) * speed * Time.deltaTime);
            front = true;
            backward = false;
        }
        if (brake)
        {
            if (front == true && backward == false)
            {
                speed = 3;
            }
            speed += ((speedChange * Time.deltaTime) * 2);
            /*if (speed < 0)
            {
                speed = 0;
            }*/
            rb.velocity -= new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * speed * Time.deltaTime, Mathf.Sin(Mathf.Deg2Rad * angle) * speed * Time.deltaTime);
            front = false;
            backward = true;
        }
        if (angle < 0)
        {
            angle += 360;
        }
        angle = angle % 360;
        rb.rotation = angle;
    }

    void ScorePoints(int pointsToAdd)
    {
        score+=pointsToAdd;
        points.text = "Points: "+score;
    }

    IEnumerator StartBlinking()
    {
        yield return new WaitForSeconds(.5f); //However many seconds you want
        death = false;
        player.GetComponent<SpriteRenderer>().enabled = !player.GetComponent<SpriteRenderer>().enabled;
        /*rearbooster.GetComponent<SpriteRenderer>().enabled = !rearbooster.GetComponent<SpriteRenderer>().enabled;
        frontboost.GetComponent<SpriteRenderer>().enabled = !frontboost.GetComponent<SpriteRenderer>().enabled;
        frontboost1.GetComponent<SpriteRenderer>().enabled = !frontboost1.GetComponent<SpriteRenderer>().enabled;*///This toggles it
        StartCoroutine(StartBlinking());
    }

    IEnumerator GetInvulnerable()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        GetComponent<Collider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        death = true;
        StopAllCoroutines();
    }

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(3f);
        int random = Random.Range(0, 4);
        if (random == 1)
        {
            Instantiate(asteroidSmall, position, transform.rotation);
            Instantiate(asteroidSmall, position, transform.rotation);
        }
        if (random == 2)
        {
            Instantiate(asteroidMedium, position, transform.rotation);
            Instantiate(asteroidMedium, position, transform.rotation);
        }
        if (random == 3)
        {
            Instantiate(asteroidLarge, position, transform.rotation);
            Instantiate(asteroidLarge, position, transform.rotation);
        }
        StartCoroutine(Spawning());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lives > 0)
        {
            GameObject smallExp= GameObject.FindGameObjectWithTag("small");
            GameObject mediumExp = GameObject.FindGameObjectWithTag("medium");
            GameObject largeExp = GameObject.FindGameObjectWithTag("large");
            if (collision.gameObject.tag.Equals("BigAsteroid"))
            {
                StartCoroutine(StartBlinking());
                StartCoroutine(GetInvulnerable());
                speed = 0;
                GameObject ex = GameObject.FindWithTag("Big Explosion");
                ex.GetComponent<SpriteRenderer>().enabled = true;
                ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                ex.GetComponent<Animator>().SetInteger("Explosion", 1);
                //mediumExp.transform.position = GameObject.FindGameObjectWithTag("player").transform.position;
                transform.position = OgPos;
                lives--;
            }
            if (collision.gameObject.tag.Equals("MediumAsteroid"))
            {
                StartCoroutine(StartBlinking());
                StartCoroutine(GetInvulnerable());
                speed = 0;
                GameObject ex = GameObject.FindWithTag("Big Explosion");
                ex.GetComponent<SpriteRenderer>().enabled = true;
                ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                ex.GetComponent<Animator>().SetInteger("Explosion", 1);
                transform.position = OgPos;
                lives--;
            }
            if (collision.gameObject.tag.Equals("SmallAsteroid"))
            {
                StartCoroutine(StartBlinking());
                StartCoroutine(GetInvulnerable());
                speed = 0;
                GameObject ex = GameObject.FindWithTag("Big Explosion");
                ex.GetComponent<SpriteRenderer>().enabled = true;
                ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                ex.GetComponent<Animator>().SetInteger("Explosion", 1);
                transform.position = OgPos;
                lives--;
            }
        }
    }
}
