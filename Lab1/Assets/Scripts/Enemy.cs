using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // if player doesn't exist, stop the function
        if (PlayerLauncher.instance.player == null)
            return;
    
        // if enemy has been impacted by any collider faster than 2 meters per second AND the player is launching
        if(collision.relativeVelocity.magnitude > 2 && PlayerLauncher.instance.player.launching == true)
        {
            GameManager.instance.DestroyEnemy(this);
        }
    }
}
