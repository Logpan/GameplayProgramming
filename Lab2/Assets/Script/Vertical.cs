using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    public int maxVelocity;
    private bool bSpawn = true;

    void Update()
    {
        if(bSpawn)
        {
            OnSpawn();
            bSpawn = false;
        }
    }

    public void OnSpawn()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(-maxVelocity, -1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            switch (collision.gameObject.name)
            {
                case "Bottom":
                    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 4.5f, 0);
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(-maxVelocity, -1));
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
        Vector2 velocity = new Vector2(Random.Range(maxVelocity, -maxVelocity), Random.Range(-maxVelocity, maxVelocity));
        GameObject split1 = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        split1.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.y / 2, this.gameObject.transform.localScale.z / 2);
        split1.GetComponent<Rigidbody2D>().velocity = velocity;
        GameObject split2 = Instantiate(enemy, gameObject.transform.position + new Vector3(0.01f, 0.01f, 0.01f), Quaternion.identity);
        split2.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.y / 2, this.gameObject.transform.localScale.z / 2);
        split2.GetComponent<Rigidbody2D>().velocity = velocity;

    }
}
