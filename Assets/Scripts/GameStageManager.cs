using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    public List<GameObject> charList;

    public int gotCoin;
    public int moveCount;

    public GameObject bossStage;
    public bool isBossStage;
    private bool bossAppear;

    private GameObject player;

    private void Awake()
    {
        switch (GameManager.gameManager.selected_Char)
        {
            case "":
                GameObject Player = Instantiate(charList[0], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                player = Player;
                break;
        }

        gotCoin = 0;
        moveCount = 0;
        isBossStage = false;
        bossAppear = false;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (moveCount > 30)
        {
            //Debug.Log("3");
        }
        else if (moveCount > 20)
        {
            //Debug.Log("2");
        }
        else
        {
            //Debug.Log("1");
        }

        if (moveCount > 50)
        {
            isBossStage = true;

            if (!bossAppear)
            {
                StartCoroutine(BosscutScene());
            }
        }
    }

    private IEnumerator BosscutScene()
    {
        Camera cam = Camera.main.GetComponent<Camera>();
        GameObject backReaper = GameObject.FindObjectOfType<BackReaperMove>().gameObject;

        float degree = backReaper.transform.position.x - player.transform.position.x;

        while (degree < 7 || degree > 8)
        {
            if(degree < -9)
            {
                backReaper.transform.position = new Vector3(player.transform.position.x + 13f, 1, 0);
                backReaper.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                backReaper.transform.position -= new Vector3(0.1f * Time.deltaTime, 0, 0);
            }

            if(cam.orthographicSize > 3.5f)
            {
                cam.orthographicSize -= 0.1f * Time.deltaTime;
            }

            degree = backReaper.transform.position.x - player.transform.position.x;
            yield return null;
        }

        bossStage.SetActive(true);
        bossAppear = true;
    }
}
