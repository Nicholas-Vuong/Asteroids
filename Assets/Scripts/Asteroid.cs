using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    public float maxThrust;
    public float maxTorque;
    public Rigidbody2D rb;
    public int BigHealth = 15;
    public int MediumHealth = 10;
    public int SmallHealth = 5;
    public GameObject asteroidLarge;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public int asteroidSize;
    public int points;
    public GameObject player;
    public float x, y;
    public Vector3 position;
    public float MaxTime;
    public float TimeLeft;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRenderer2;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MaxTime = 3f;
        TimeLeft = .3f;
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
        x = -11.87f;
        y = -2.35f;
        position = new Vector3(x, y);
        player = GameObject.FindWithTag("player");
    }

    IEnumerator animation()
    {
        yield return new WaitForSeconds(.5f);
        GameObject ex = GameObject.FindWithTag("Big Explosion");
        ex.GetComponent<SpriteRenderer>().enabled = false;
        ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
        ex.GetComponent<Animator>().SetInteger("Explosion", 4);
    }
    // Update is called once per frame
    void Update()
    { 
        Vector2 newPos = transform.position;
        if (transform.position.y > 6.48f)
        {
            newPos.y = -6.48f;
        }
        if (transform.position.y < -6.48f)
        {
            newPos.y = 6.48f;
        }
        if (transform.position.x > 12.02f)
        {
            newPos.x = -12.02f;
        }
        if (transform.position.x < -12.02f)
        {
            newPos.x = 12.02f;
        }
        transform.position = newPos;

        if (BigHealth == 0)
        {
            int random = Random.Range(1, 3);
            Vector3 temp = new Vector3(1f,1f);
            GameObject ex = GameObject.FindWithTag("Big Explosion");
            ex.GetComponent<SpriteRenderer>().enabled = true;
            ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            ex.GetComponent<Animator>().SetInteger("Explosion", 2);
            StartCoroutine(animation());
            Instantiate(asteroidMedium, transform.position + temp, transform.rotation);
            Instantiate(asteroidMedium, transform.position, transform.rotation);
            player.SendMessage("ScorePoints", points);
            Destroy(gameObject);           
        }
        if (MediumHealth == 0)
        {
            Vector3 temp = new Vector3(1f, 1f);
            int random = Random.Range(1, 3);
            GameObject ex = GameObject.FindWithTag("Big Explosion");
            ex.GetComponent<SpriteRenderer>().enabled = true;
            ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            ex.GetComponent<Animator>().SetInteger("Explosion", 1);
            StartCoroutine(animation());
            Instantiate(asteroidSmall, transform.position+temp, transform.rotation);
            Instantiate(asteroidSmall, transform.position, transform.rotation);
            player.SendMessage("ScorePoints", points);
            Destroy(gameObject);
        }
        if (SmallHealth == 0)
        {
            int random = Random.Range(1, 3);
            GameObject ex = GameObject.FindWithTag("Big Explosion");
            ex.GetComponent<SpriteRenderer>().enabled = true;
            ex.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            ex.GetComponent<Animator>().SetInteger("Explosion", 0);
            StartCoroutine(animation());
            player.SendMessage("ScorePoints", points);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag.Equals("BigAsteroid") && collision.gameObject.tag.Equals("bullet"))
        {
            BigHealth -= 5;
        }
        if (gameObject.tag.Equals("MediumAsteroid") && collision.gameObject.tag.Equals("bullet"))
        {
            MediumHealth -= 5;           
        }
        if (gameObject.tag.Equals("SmallAsteroid") && collision.gameObject.tag.Equals("bullet"))
        {
            SmallHealth -= 5;
        }
    }
}
