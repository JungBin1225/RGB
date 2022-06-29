using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    float hp;
    public float fullhp;
    public float decrease_Hp;
    public Image hpBar;
    public bool isDead;
    public bool isCanBreath = true;
    public bool isCanTouchGhost= false;
    public bool isBreath;
    public float[] changeDecreaseHpSpeedCount;
    int changeCount = 1;
    public int deathType;

    ScoreMgr scoreMgr;
    PlayerMove pMove;
    GameStageManager stageMg;

    void Start()
    {
        scoreMgr = FindObjectOfType<ScoreMgr>();
        pMove = FindObjectOfType<PlayerMove>();
        stageMg = FindObjectOfType<GameStageManager>();
        hp = fullhp;
        hpBar = GameObject.Find("HPBAR").GetComponent<Image>();
        UpdateHP();
        //StartCoroutine(DecreaseHealth());
    }

    void Update()
    {
        if(hp > fullhp)
        {
            hp = fullhp;
        }

        if(!stageMg.isBossStage)
        {
            UpdateHP();
            hp -= Time.deltaTime * decrease_Hp;
        }
        else
        {
            hpBar.transform.parent.gameObject.SetActive(false);
        }

        //Debug.Log(hp);
    }

    private void UpdateHP()
    {
        hpBar.fillAmount = hp / fullhp;
    }

    public void GetDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0 && isCanBreath)
        {
            deathType = 0;
            GoDown();
        }
        else if (hp >= fullhp)
        {
            hp = fullhp;
        }
        //Debug.Log(hp);
        UpdateHP();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        Ghost ghost = collision.GetComponent<Ghost>();
        if (isCanBreath && ghost)
            MeetGhost(ghost);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BackReaperMove>() || (isCanBreath && collision.GetComponent<FrontReaper>()))
        {
            if (collision.GetComponent<BackReaperMove>())
            deathType = 1;
            if(isCanBreath && collision.GetComponent<FrontReaper>())
            deathType = 2;

            GoDown();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Ghost ghost = collision.GetComponent<Ghost>();
        if (ghost)
        {
            Debug.Log("Bye Ghost");
            ghost.cantouch = false;
        }
    }

    private void MeetGhost(Ghost ghost)
    {
        isCanTouchGhost = true;
        StartCoroutine(Ghost(ghost));
    }
    IEnumerator Ghost(Ghost ghost)
    {
        if (ghost.cantouch == false) yield break;
        while (ghost.cantouch && ghost.gage <= ghost.full)
        {
            Debug.Log("Meet Ghost");
            if(isBreath)
            {
                ghost.GetGage(Time.deltaTime * 0.5f);
            }
            else
            {
                ghost.GetGage(Time.deltaTime * -0.5f);
            }
            yield return null;
        }
        if (ghost.gage >= ghost.full)
        {
            scoreMgr.SetCombo(scoreMgr.GetCombo()*2);

        }
        ghost.Gone();
    }
    public void GoDown()
    {
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(Down());
        }
    }
    IEnumerator Down()
    {
        while (transform.position.y > -2)
        {
            transform.Translate(0, Time.deltaTime * -10, 0);
            yield return null;
        }
        //Debug.Log(" YOU  DIED");
        gameObject.SetActive(false);
        scoreMgr.GoEnding();
    }
}
