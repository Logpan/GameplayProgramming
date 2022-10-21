using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public bool launching;

    // Update is called once per frame
    void Update()
    {
        if (launching && rig.IsSleeping()) 
        {
            GameManager.instance.PlayerFinished();
            Destroy(gameObject);
        }
    }
    
    public void Launch(Vector2 direction)
    {
        rig.isKinematic = false;
        rig.AddForce(direction * 5, ForceMode2D.Impulse);
        launching = true;
        StartCoroutine(destroyPlayerAfterTime());
    }

    IEnumerator destroyPlayerAfterTime()
    {
        yield return new WaitForSeconds(6);
        GameManager.instance.PlayerFinished();
        Destroy(gameObject);
    }

    void Awake()
    {
        rig.isKinematic = true;    
    }
}
