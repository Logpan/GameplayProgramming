using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(this.gameObject.tag == "Bullet")
        {
            rb.velocity = new Vector2(0, 5);
            rb.rotation = 35;
        }
        else if(this.gameObject.tag == "Bullet1")
        {
            rb.rotation = 0;
            rb.velocity = new Vector2(0, 5);
        }
        else if (this.gameObject.tag == "Bullet2")
        {
            if (GameController.instance.player.transform.position.x == gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(0, 3);
            }
            else if (GameController.instance.player.transform.position.x < gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(3, 3);
            }
            else if (GameController.instance.player.transform.position.x > gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-3, 3);
            }
            rb.rotation = 0;
        }
        else if (this.gameObject.tag == "split")
        {
            rb.velocity = new Vector2(Random.Range(-5, 5), Random.Range(1, 5));
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (this.gameObject.tag == "Bullet" || this.gameObject.tag == "Bullet2" || this.gameObject.tag == "split")
            {
                GameObject.Destroy(this.gameObject);
            }
            else if (this.gameObject.tag == "Bullet1")
            {
                if (transform.localScale.x > 0.5f)
                    split(this.gameObject);
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public void split(GameObject bullet)
    {
        Vector2 velocity = new Vector2(0, 5);
        GameObject split1 = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        split1.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.y / 2, this.gameObject.transform.localScale.z / 2);
        split1.transform.position = new Vector3(split1.transform.position.x + 1, split1.transform.position.y, 0);
        split1.tag = "split";
        GameObject split2 = Instantiate(bullet, gameObject.transform.position + new Vector3(0.01f, 0.01f, 0.01f), Quaternion.identity);
        split2.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.y / 2, this.gameObject.transform.localScale.z / 2);
        split2.transform.position = new Vector3(split1.transform.position.x - 1, split1.transform.position.y, 0);
        split2.tag = "split";
        Destroy(split1, 2f);
        Destroy(split2, 2f);

    }


    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "split")
        {
            transform.Rotate(new Vector3(0, 0, 20) * Random.Range(-5, 5));
        }
    }
}
