using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackReaperMove : MonoBehaviour
{
    public float speed;
    public float[] speeds;
    public float[] changeSpeedCount;
    int changeCount=0;
    public float time;
    PlayerMove playerMove;
    ScoreMgr scoreMgr;
    GameStageManager manager;
    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        scoreMgr = FindObjectOfType<ScoreMgr>();
        manager = FindObjectOfType<GameStageManager>();
    }
    void Update()
    {
        time += Time.deltaTime;
        scoreMgr.ReaperUIUpdate();
        if (changeCount < speeds.Length)
        {
            if (manager.moveCount > changeSpeedCount[changeCount])
            {
                speed = speeds[changeCount];
                changeCount++;
            }
        }

        if(!manager.isBossStage)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);
        }
    }
    public void SetSpeed(float spd)
    {
        speed = spd;
    }
}
