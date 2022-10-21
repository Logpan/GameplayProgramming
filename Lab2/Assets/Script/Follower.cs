using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(GameController.instance.player != null)
            rb.velocity = 
                new Vector2(GameController.instance.player.transform.position.x - transform.position.x,
                GameController.instance.player.transform.position.y - transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Bullet1" || collision.tag == "Bullet2" || collision.tag == "split")
        {
            Destroy(gameObject);
            GameController.instance.AddScore(100);
        }
    }
}