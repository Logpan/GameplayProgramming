using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    public float offset = 2.0f;
    public static CameraController instance;

    public Transform mapView;
    public bool isInMapView = false;
    bool returnFromMapView = false;
    bool doCo = false;

    private void Awake()
    {
        instance = this;      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player == null)
            return;
        if(player.launching && player.transform.position.x >= transform.position.x - offset
            && !isInMapView)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offset, 1, -10), Time.deltaTime*10);
        }
        else if (isInMapView)
        {
            if (!returnFromMapView) transform.position = Vector3.Lerp(transform.position, new Vector3(mapView.transform.position.x, transform.position.y, -10), Time.deltaTime * 10);
            if (!doCo) StartCoroutine(waitForMapView());
            if (returnFromMapView)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0f , transform.position.y, -10f), Time.deltaTime * 10);
                if (transform.position.x > -0.5 && transform.position.x < 0.5)
                {
                    returnFromMapView = false;
                    instance.isInMapView = false;
                    doCo = false;
                }
            }
        }

    }

    IEnumerator waitForMapView()
    {
        doCo = true;
        yield return new WaitForSeconds(5);
        returnFromMapView = true;
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
        Vector3 newPos = player.transform.position;
        newPos.z = -10;
        transform.position = newPos;
    }

    public void SeeMap()
    {
        if (!player.launching && !isInMapView)
        {
            isInMapView = true;
        }
    }

}
