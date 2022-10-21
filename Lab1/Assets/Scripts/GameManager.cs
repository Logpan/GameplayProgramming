using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> availablePlayers = new List<GameObject>();
    public static GameManager instance;

    public List<GameObject> psPrefabs;
    public Transform particleTransform;

    public LineRenderer lr;
    public LineRenderer lr2;
    PlayerLauncher pl;
    public float radius;

    void Awake()
    {
        instance = this;    
    }

    public void DestroyEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyBlock(Block block)
    {
        GameObject g;
        switch (block.type)
        {
            case Block.Type.WOOD:
                g = Instantiate(psPrefabs[0], particleTransform);
                g.transform.position = block.transform.position;
                break;
            case Block.Type.METAL:
                g = Instantiate(psPrefabs[1], particleTransform);
                g.transform.position = block.transform.position;
                break;
            case Block.Type.BLUE:
                g = Instantiate(psPrefabs[2], particleTransform);
                g.transform.position = block.transform.position;
                break;
            case Block.Type.BOOM:
                g = Instantiate(psPrefabs[3], particleTransform);
                g.transform.position = block.transform.position;
                break;
        }
        
        Destroy(block.gameObject);
    }

    public void SpawnNewPlayer()
    {
        PlayerLauncher.instance.SetNewPlayer(availablePlayers[0]);
        availablePlayers.RemoveAt(0);
        lr.enabled = true;
        lr2.enabled = true;
    }

    public void PlayerFinished()
    {
        if (availablePlayers.Count > 0 && enemies.Count > 0)
        {
            GameUI.instance.nextPlayerButton.SetActive(true);
        }
        else
        {
            GameUI.instance.SetEndScreen(enemies.Count == 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewPlayer();
        pl = PlayerLauncher.instance;
    }

    private void Update()
    {
        if (lr.enabled)
        {
            if (pl.player.launching)
            {
                lr.enabled = false;
                lr2.enabled = false;
            }
        }
        else
        {
            if (!pl.player.launching)
            {
                lr.enabled = true;
                lr2.enabled = true;
            }
        }


        if (!pl.player.launching)
        {
            lr.SetPosition(1, new Vector3(pl.player.transform.position.x + ( (pl.player.transform.position.x < 0) ? -radius : +radius), pl.player.transform.position.y, 0));
            lr2.SetPosition(1, new Vector3(pl.player.transform.position.x + ( (pl.player.transform.position.x < 0) ? -radius : +radius), pl.player.transform.position.y, 0));
        }
    }
}
