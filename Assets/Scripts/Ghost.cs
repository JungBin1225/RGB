using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public float gage = 0;
    public  float full = 1;
    public GameObject bar;
    public bool cantouch=true;
    private GameStageManager manager;

    public void OnEnable()
    {
        manager = GameObject.Find("StageManager").GetComponent<GameStageManager>();
        transform.position =new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
        cantouch = true;
        gage = 0;
        bar.transform.localScale = new Vector3(0,0.1f,0);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }

    void Update()
    {
        if (manager.isBossStage)
        {
            Destroy(this.gameObject);
        }
    }

    public void GetGage(float f)
    {
        gage += f;
        if(gage < 0)
        {
            gage = 0;
        }
        bar.transform.localScale = new Vector3(gage, bar.transform.localScale.y, bar.transform.localScale.z);
    }

    internal void Gone()
    {
        float time = 1;
        StartCoroutine(GoneUp(time));
    }
    IEnumerator GoneUp(float a)
    {
        Debug.Log("GoneUp");
        while (a >=0)
        {
            a -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,a);
            transform.Translate(0, Time.deltaTime, 0);
            yield return null;
        }
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
