using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("BigAsteroid"))
        { 
            Destroy (gameObject);
        }
        if (collision.gameObject.tag.Equals("MediumAsteroid"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag.Equals("SmallAsteroid"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag.Equals("bullet"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag.Equals("player"))
        {
            Destroy(gameObject);
        }
    }
}
