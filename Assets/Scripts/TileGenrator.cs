using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileType { BLUE, RED }
public class TileGenrator : MonoBehaviour
{
    public List<Tile> tiles;
    public Tile tile;
    public int SpawnCount;
    public Transform playerPos;
    int nowCount = 0;
    public float beforeY = 0;
    public int moveTile;

    int max = 3;
    int min = 0;

    int x=0;

    public void Start()
    {
        tiles = new List<Tile>();
        InstantiateFabs();
    }
    public void InstantiateFabs()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            Tile tile_= Instantiate(tile, this.transform);
            tile_.transform.position = new Vector3(i, tile.transform.position.y, tile.transform.position.z);
            //Debug.Log(tile_);
            tiles.Add(tile_);
            tiles[i].gameObject.SetActive(false);
        }
        InitFabs();
    }
    public void InitFabs()
    {
        //8개 생성
        
        //첫스타트
        tiles[nowCount].gameObject.SetActive(true);
        tiles[nowCount].transform.position = new Vector2(0, beforeY);
        tiles[nowCount].SetBlack();

        for (int i = 1; i < 12; i++)
        {
            FabOn(false, moveTile);
        }
    }
    public void FabOn(bool arrive, int moveTile)
    {
        ++nowCount;
        ++x;
        if (nowCount >= tiles.Count)
        {
            nowCount = 0;
        }

        tiles[nowCount].GetComponent<Tile>().arrive = arrive;
        tiles[nowCount].GetComponent<Tile>().moveTile = moveTile;
        tiles[nowCount].gameObject.SetActive(false);
        tiles[nowCount].gameObject.SetActive(true);
        tiles[nowCount].transform.position = new Vector2(x, beforeY);
        tiles[nowCount].SetStartXPoint(x);


        int randnum = Random.Range(-1, 2);

        //Debug.Log(randnum);

        if (tiles[nowCount].transform.position.y + randnum > max) //randnum이 양수
        {
            randnum = Random.Range(-randnum, 1);
        }
        else if (tiles[nowCount].transform.position.y + randnum < min) //randnum이 음수
        {
            randnum = Random.Range(0, -randnum);
        }

        tiles[nowCount].transform.position = new Vector2(x, tiles[nowCount].transform.position.y + randnum);

        beforeY = tiles[nowCount].transform.position.y;
  
    }
    public int GetNowCount()
    {
        return nowCount;
    }
}
