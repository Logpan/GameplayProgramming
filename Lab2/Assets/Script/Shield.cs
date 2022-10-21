using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject shield;

    private void Start()
    {
        shield = GameController.instance.player.transform.Find("Shield").gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(shield.activeSelf)
            {
                if (shield.transform.Find("ShieldBreak").gameObject.activeSelf)
                {
                    shield.transform.Find("ShieldBreak").gameObject.SetActive(false);
                    shield.SetActive(false);
                }
                else
                {
                    shield.transform.Find("ShieldBreak").gameObject.SetActive(true);
                }
            }
            else
            {
                GameController.instance.player.transform.Find("Shield").gameObject.SetActive(true);
            }
        }
    }
}
