using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmCheck : MonoBehaviour
{
    public bool touchAble;
    private GameObject touchedNote;
    ScoreMgr scoreMgr;

    private bool rightDown;
    private bool rightUp;
    private bool leftDown;
    private bool leftUp;

    void OnEnable()
    {
        scoreMgr = FindObjectOfType<ScoreMgr>();

        touchAble = false;
        rightDown = false;
        rightUp = false;
        leftDown = false;
        leftUp = false;
    }

    void Update()
    {
        if(touchAble)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                leftDown = true;
                leftUp = false;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                rightDown = true;
                rightUp = false;
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                leftUp = true;
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                rightUp = true;
            }

            OnTouch();
        }
        else
        {
            rightDown = false;
            rightUp = false;
            leftDown = false;
            leftUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchAble = true;
        touchedNote = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touchAble = false;
        touchedNote = null;
    }

    public void OnTouch()
    {
        string type = touchedNote.GetComponent<RhythmNote>().type;

        if(type == "Red")
        {
            if(leftDown && leftUp && !rightDown)
            {
                scoreMgr.AddScore(100);
                Destroy(touchedNote);
                touchedNote = null;
            }
        }
        else if(type == "Blue")
        {
            if (rightDown && rightUp && !leftDown)
            {
                scoreMgr.AddScore(100);
                Destroy(touchedNote);
                touchedNote = null;
            }
        }
        else
        {
            if(leftDown && rightDown && !leftUp && !rightUp)
            {
                scoreMgr.AddScore(100);
                Destroy(touchedNote);
                touchedNote = null;
            }
        }
    }
}
