using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreMgr : MonoBehaviour
{
    int score;
    int combo;
    public Text scoreText;
    public Text comboText;
    public Text distanceText;
    public Sprite[] comboSp;
    public Sprite[] reaperSp;
    public Image comboImg;
    public GameObject siren;
    public int[] changeComboCount;
    public int[] changeComboHealthList;
    public bool gameOver;
    PlayerMove pm;
    PlayerHealth ph;
    GameStageManager manager;

    public GameObject ending;
    public Image[] endImgs;
    public Text endScoreText;
    public Text endreasonText;

    public GameObject[] endBGM;

    public Image DodgeImage;
    private void Awake()
    {
        pm = FindObjectOfType<PlayerMove>();
        ph = FindObjectOfType<PlayerHealth>();
        manager = FindObjectOfType<GameStageManager>();
        siren = GameObject.Find("siren");
        gameOver = false;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "점수 : " + score;

        if(manager.isBossStage)
        {
            combo = 0;
        }

        if (combo >= 1)
        {
            comboText.gameObject.SetActive(true);
            comboText.text = "콤보 : " + combo;
            comboImg.sprite = comboSp[GetComboIndex()];

        }
        else
        {
            comboText.gameObject.SetActive(false);
            comboImg.sprite = comboSp[0];
        }
    }

    public void ReaperUIUpdate()
    {
        if (manager.isBossStage)
        {
            distanceText.gameObject.SetActive(false);
        }
        else
        {
            distanceText.text = "거리 : " + GetDistancePtoR();
        }

        if(manager.isBossStage)
        {
            siren.SetActive(false);
        }
        else if (GetDistancePtoR() < 5)
        {
            siren.SetActive(true);
            siren.GetComponent<Animator>().SetBool("near", true);
        }
        else if (GetDistancePtoR() < 10)
        {
            siren.SetActive(true);
            siren.GetComponent<Animator>().SetBool("near", false);
        }
        else
        {
            siren.SetActive(false);
        }
    }

    public int GetDistancePtoR()
    {
        return (int)Vector3.Distance(pm.transform.position, FindObjectOfType<BackReaperMove>().transform.position);
    }

    public void AddScore()
    {
        this.score += 1 + combo;
        UpdateUI();
    }

    public void AddScore(int score)
    {
        this.score += score;
        UpdateUI();
    }

    public void SetCombo(int c)
    {
        combo = c;
        UpdateUI();
    }

    public int GetCombo()
    {
        return combo;
    }

    public int GetComboHeathAmount()
    {
        return changeComboHealthList[GetComboIndex()];
    }

    public int GetComboIndex()//인덱스 4까지.
    {
        int i = 0;
        for (; i < changeComboCount.Length; i++)
        {
            if (combo > changeComboCount[i])
            {
                continue;
            }
            else
            {
                break;
            }
        }
        //Debug.Log();
        return i;
    }

    public void GoEnding()
    {
        gameOver = true;

        endBGM[0].gameObject.SetActive(false);
        endBGM[1].gameObject.SetActive(true);
        ending.SetActive(true);
        if (ph.deathType == 0)
        {
            endImgs[1].enabled = false;
            endImgs[2].enabled = false;
            endreasonText.text = "동력원이 꺼졌습니다.";
        }
        else if(ph.deathType == 1)
        {
            endImgs[0].enabled = false;
            endImgs[2].enabled = false;
            endreasonText.text = "체포되었습니다.";
        }
        else
        {
            endImgs[0].enabled = false;
            endImgs[1].enabled = false;
            endreasonText.text = "발각되었습니다.";
        }

        endScoreText.text = "점수 : "+score;
    }
    public void GoMainScene()
    {
        SceneManager.LoadScene(2);
    }
}
