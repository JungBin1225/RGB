using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIme : MonoBehaviour
{
    int timer = 0;
    public Text text;

    PlayerMove pMove;
    void Start()
    {
        pMove = FindObjectOfType<PlayerMove>();
        timer = 0;
    }

    void Update()
    {
        if(timer == 0)
        {
            //Debug.Log("Start");
            pMove.isMove = true;
            Time.timeScale = 0.0f;
        }
        if (timer <= 300)
        {
            timer++;
            //Debug.Log(timer);

            if (timer < 100)
                text.text = "3";
            else if (timer < 200)
                text.text = "2";
            else if (timer < 300)
                text.text = "1";
        }
        else
        {
            Time.timeScale = 1.0f;
            pMove.isMove = false;
            gameObject.SetActive(false);
        }
    }
}
