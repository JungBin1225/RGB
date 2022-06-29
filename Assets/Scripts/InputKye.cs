using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKye : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerHealth pHealth;
    GameStageManager stageMg;
    public RhythmCheck rhythmCheck;

    private bool rightDown;
    private bool leftDown;
    private bool isBreath;

    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        pHealth = FindObjectOfType<PlayerHealth>();
        stageMg = FindObjectOfType<GameStageManager>();
        rightDown = false;
        leftDown = false;
        isBreath = false;
        pHealth.isBreath = false;
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMove.CantBreath();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            playerMove.CanBreath();
        }*/

        if(Input.GetKeyDown(KeyCode.G))
        {
            leftDown = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            rightDown = true;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            leftDown = false;
            playerMove.CanBreath();
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            rightDown = false;
            playerMove.CanBreath();
        }

        Move();
    }

    private void Move()
    {
        if (leftDown && rightDown)
        {
            if (!isBreath)
            {
                if (!stageMg.isBossStage)
                {
                    playerMove.CantBreath();
                }
                isBreath = true;
                pHealth.isBreath = true;
            }
        }
        else
        {
            if (!isBreath && !playerMove.isMove)
            {
                if (Input.GetKeyUp(KeyCode.G))
                {
                    if(!stageMg.isBossStage)
                    {
                        playerMove.SetTileType(1);
                        playerMove.Moving1cm2();
                    }
                    else
                    {
                        //rhythmCheck.OnTouch("Red");
                    }
                }
                if (Input.GetKeyUp(KeyCode.H))
                {
                    if (!stageMg.isBossStage)
                    {
                        playerMove.SetTileType(0);
                        playerMove.Moving1cm2();
                    }
                    else
                    {
                        //rhythmCheck.OnTouch("Blue");
                    }
                }
            }
        }

        if (isBreath)
        {
            if (!leftDown && !rightDown)
            {
                isBreath = false;
                pHealth.isBreath = false;
            }
        }
    }
}
