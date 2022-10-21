using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1;
    private float movementX;
    private float movementY;
    public List<GameObject> Bullet = new List<GameObject>();
    public int iBulletChoice = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnFire()
    {
        StartCoroutine(Fire());        
    }

    IEnumerator Fire()
    {
        switch(iBulletChoice)
        {
            case 0:
            case 1:
                Vector3 bulletPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + transform.localScale.x + 0.1f, 0);
                GameObject bullet = Instantiate(Bullet[iBulletChoice], bulletPosition, Quaternion.identity);
                Destroy(bullet, 2f);
                break;
            case 2:
                Vector3 bulletLeftPosition = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + transform.localScale.x + 0.1f, 0);
                GameObject bulletLeft = Instantiate(Bullet[iBulletChoice], bulletLeftPosition, Quaternion.identity);
                
                Vector3 bulletRightPosition = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + transform.localScale.x + 0.1f, 0);
                GameObject bulletRight = Instantiate(Bullet[iBulletChoice], bulletRightPosition, Quaternion.identity);

                Vector3 bulletMidPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + transform.localScale.x + 0.1f, 0);
                GameObject bulletMid = Instantiate(Bullet[iBulletChoice], bulletMidPosition, Quaternion.identity);
                
                Destroy(bulletLeft, 2f);
                Destroy(bulletRight, 2f);
                Destroy(bulletMid, 2f);
                break;
        }
        
        yield return new WaitForSeconds(0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Choice1")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Choice(1));
        }
        else if (collision.tag == "Choice2")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Choice(2));
        }
        else if (collision.tag == "Choice3")
        {
            Destroy(collision.gameObject);
            transform.Find("Shield").gameObject.SetActive(true);
        }
    }

    IEnumerator Choice(int iBullet)
    {
        iBulletChoice = iBullet;
        yield return new WaitForSeconds(5f);
        iBulletChoice = 0;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementY);
        rb.MovePosition(transform.position + movement * speed *Time.fixedDeltaTime);
    }
}
