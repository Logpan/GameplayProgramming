using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Shield")
        {
            GameObject.Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            GameController.instance.nbAmmo += 10;
            GameController.instance.DestroyPlayer();
            GameObject.Destroy(gameObject);
        }
        else if (collision.tag == "Bullet" || collision.tag == "Bullet1" || collision.tag == "split")
        {
            if(Random.Range(0,7) == 6)
            {
                Instantiate(GameController.instance.collectable[Random.Range(0, GameController.instance.collectable.Count)], gameObject.transform.position, Quaternion.identity);
            }
            else if (Random.Range(0, 3) == 2)
            {
                Instantiate(GameController.instance.ammo, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
