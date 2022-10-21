using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    int maxVelocity;
    Rigidbody2D rb;

    public void Start()
    {
        maxVelocity = GameController.instance.maxVelocity;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, Random.Range(-maxVelocity, -1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            switch (collision.gameObject.name)
            {
                case "Bottom":
                    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 4.5f, 0);
                    rb.velocity = new Vector2(0, Random.Range(-maxVelocity, -1));
                    break;
            }
        }
        else if (collision.tag == "Bullet" || collision.tag == "Bullet1" || collision.tag == "Bullet2" || collision.tag == "split")
        {
            if (this.gameObject.transform.localScale.x > 0.3)
            {
                split(this.gameObject);
            }
            Destroy(this.gameObject);
            GameController.instance.AddScore(50);
        }
    }

    public void split(GameObject enemy)
    {
        GameObject split1 = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        split1.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.y / 2, this.gameObject.transform.localScale.z / 2);

        GameObject split2 = Instantiate(enemy, gameObject.transform.position + new Vector3(0.01f, 0.01f, 0.01f), Quaternion.identity);
        split2.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.y / 2, this.gameObject.transform.localScale.z / 2);

    }
}
