using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType tileType;
    public Sprite[] Sprites; //0 b 1 r 2 black
    public Sprite black; //0 b 1 r 2 black
    public float startXPoint;
    public bool arrive;
    public int moveTile;
    public GameObject ghostPrefab;
    public GameObject coinPrefab;
    private bool isGhost;
    TileGenrator tile;
    BackReaperMove reaper;

    Ghost ghost;
    Coin coin;

    private void Awake()
    {
        ghost = transform.GetChild(0).GetComponent<Ghost>();
        coin = transform.GetChild(1).GetComponent<Coin>();
        reaper = FindObjectOfType<BackReaperMove>();
        tile = FindObjectOfType<TileGenrator>();

        isGhost = false;
    }
    public void OnEnable()
    {
        SetRandomTileType();
        isGhost = false;
        InstantiateGhost(arrive, moveTile);

        if(!isGhost)
        {
            InstantiateCoin();
        }
    }

    public void OnDisable()
    {
        ghost.gameObject.SetActive(false);
        coin.gameObject.SetActive(false);
    }

    public void SetRandomTileType()
    {
        int r = UnityEngine.Random.Range(0, 2);
        if (r == 0)
        {
            tileType = TileType.BLUE;
            GetComponent<SpriteRenderer>().sprite = Sprites[0];
        }
        else
        {
            tileType = TileType.RED;
            GetComponent<SpriteRenderer>().sprite = Sprites[1];
        }
        //Debug.Log(r);
    }

    public void InstantiateGhost(bool arrive, int moveTile)
    {
        if(arrive)
        {
            int rand = UnityEngine.Random.Range(0, 60 / moveTile);
            //Debug.Log("asdasda");
            if (rand == 0)
            {
                //ghost.gameObject.SetActive(true);
                GameObject npc = Instantiate(ghostPrefab, this.transform);
                isGhost = true;
            }
        }
    }

    public void InstantiateCoin()
    {
        if(transform.position.x != 0)
        {
            int rand = UnityEngine.Random.Range(0, 10);
            if (rand == 0)
            {
                //coin.gameObject.SetActive(true);
                GameObject coin = Instantiate(coinPrefab, this.transform);
            }
        }
    }

    private void GetGhost()
    {
        Ghost gh = Instantiate(ghost, this.transform);
        gh.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        //Debug.Log("Get Ghost!");
    }

    public void Update()
    {
        if(gameObject.activeSelf && tile.tiles[tile.GetNowCount()].transform.position.x - startXPoint >= tile.SpawnCount)
        {
            GoDown();
        }
    }
    public void SetBlack()
    {
        GetComponent<SpriteRenderer>().sprite = black;
    }
    public void SetStartXPoint(float x)
    {
        startXPoint = x;
    }
    public void GoDown()
    {
        StartCoroutine(Down());
    }
    IEnumerator Down()
    {
        while(transform.position.y > -2)
        {
            transform.Translate(0, Time.deltaTime * -5, 0);
            yield return null;
        }
            GetComponent<SpriteRenderer>().sprite = Sprites[0];
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("rrrrrrperrr");
        if(collision.GetComponent<BackReaperMove>())
        {
            GoDown();
        }
    }
}
