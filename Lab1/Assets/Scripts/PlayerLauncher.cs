using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    public Transform playerStartPos;
    public Player player;
    public bool holdingPlayer;
    private Camera cam;

    public static PlayerLauncher instance;

    Vector3 touchWorldPosition;


    public void SetNewPlayer(GameObject playerPrefabs)
    {
        player = Instantiate(playerPrefabs, playerStartPos.position, Quaternion.identity).GetComponent<Player>();
        if (player) CameraController.instance.SetPlayer(player);
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (InputUp() && holdingPlayer)
        {
            holdingPlayer = false;
            player.Launch(playerStartPos.position - player.transform.position);
        }

        if (InputDown() && !player.launching)
        {

            if (Input.touchCount > 0)
            {
                touchWorldPosition = cam.ScreenToWorldPoint(Input.touches[0].position);
            }
            else
            {
                touchWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            touchWorldPosition.z = 0;

            if (Vector3.Distance(touchWorldPosition, player.transform.position) <= 3f)
            {
                holdingPlayer = true;
            }
        }

        if (holdingPlayer && !player.launching)
        {
            Vector3 newPos;
            if (Input.touchCount > 0)
            {
                newPos = cam.ScreenToWorldPoint(Input.touches[0].position);
            }
            else
            {
                newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            newPos.z = 0;
            player.transform.position = newPos;

        }
        
    }

    bool InputDown()
    {
        if (CameraController.instance.isInMapView)
            return false;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    bool InputUp()
    {
        if (CameraController.instance.isInMapView)
            return false;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            return true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            return true;
        }
        return false;
    }


}
