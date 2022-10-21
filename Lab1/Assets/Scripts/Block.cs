using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Type
    {
        WOOD, BLUE, METAL, BOOM
    }
    public Type type;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == Type.BOOM && 
            PlayerLauncher.instance.player.launching == true
            && collision.relativeVelocity.magnitude > 4)
        {
            Collider2D[] colls = new Collider2D[50];
            Physics2D.OverlapCircleNonAlloc(transform.position, 1f, colls);

            foreach (Collider2D c in colls)
            {
                if (c == null || c.tag == "BOOM")
                {
                    GameManager.instance.DestroyBlock(this);
                    continue;
                }
                if (c.tag == "Ground" || c.tag == "BOOM")
                    continue;
                Debug.Log(c.name);
                Vector2 force = (c.transform.position - transform.position).normalized * 25;
                c.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                
            }
            return;
            
        }

        if (collision.relativeVelocity.magnitude > 9 && PlayerLauncher.instance.player.launching == true)
        {
            GameManager.instance.DestroyBlock(this);
        }
        if (collision.gameObject.tag != "Player")
            return;
        // if player doesn't exist, stop the function
        if (PlayerLauncher.instance.player == null)
            return;
        
        // if enemy has been impacted by any collider faster than 2 meters per second AND the player is launching
        if (collision.relativeVelocity.magnitude > 4 && PlayerLauncher.instance.player.launching == true)
        {
            GameManager.instance.DestroyBlock(this);
        }
    }
}
