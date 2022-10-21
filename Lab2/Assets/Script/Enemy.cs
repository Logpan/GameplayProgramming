using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxVelocity;
    public List<GameObject> collectable = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameController.instance.DestroyPlayer();       
        }
        else if (collision.tag == "Bullet" || collision.tag == "Bullet1" || collision.tag == "split")
        {
            if(Random.Range(0,7) == 6)
            {
                Instantiate(collectable[Random.Range(0, collectable.Count)], gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
