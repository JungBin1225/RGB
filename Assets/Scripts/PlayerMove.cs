using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float speed = 10;
    float movetime = 0.1f;

    public int moveTile;
    public int damage;
    int tileType;
    float nextPosX;
    float nextPosY;
    bool isRight;
    public bool isMove;

    public Transform cameraTf;
    TileGenrator tileGenrator;

    Animator animator;
    ScoreMgr scoreMgr;
    PlayerHealth pHealth;
    GameStageManager manager;
    //public int moveCount=0;

    public AudioClip[] clips;
    public AudioSource sources;
    public AudioSource sources2;

    public SpriteRenderer ring;
    public Sprite[] rings;

    public Sprite[] dodgeSprites;

    public GameObject dodgeBtn;

    //Vector2 rayStartPos = new Vector2(1, 0);
    private void Awake()
    {
        sources = GetComponent<AudioSource>();
        sources2 = GetComponent<AudioSource>();
        tileGenrator = FindObjectOfType<TileGenrator>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        scoreMgr = FindObjectOfType<ScoreMgr>();
        pHealth = GetComponent<PlayerHealth>();
        dodgeBtn = GameObject.Find("Dodge");
        cameraTf = Camera.main.transform;
        manager = FindObjectOfType<GameStageManager>();

        tileGenrator.moveTile = moveTile;
        isMove = false;
    }
    private void Start()
    {
        transform.position = Vector3.zero;
    }

    public void CanBreath()
    {
        pHealth.isCanBreath = true;
        animator.SetBool("Breath", true);
        //Debug.Log("Can Breath");
        scoreMgr.DodgeImage.sprite = dodgeSprites[0];
        dodgeBtn.GetComponent<Brtton>().button_reaelesd();
    }
    public void CantBreath()
    {
        if(!isMove)
        {
            sources.volume = 1;
            sources.PlayOneShot(clips[2]);

            pHealth.isCanBreath = false;
            animator.SetBool("Breath", false);
            //Debug.Log("NO Breath");
            scoreMgr.DodgeImage.sprite = dodgeSprites[1];
            dodgeBtn.GetComponent<Brtton>().button_touch();
        }
    }

    public void SetTileType(int i)
    {
        tileType = i;
    }
    public void Moving1cm2()
    {
        if (pHealth.isDead || !pHealth.isCanBreath || isMove) return;

        scoreMgr.AddScore();
        scoreMgr.SetCombo(scoreMgr.GetCombo()+1);
        if (scoreMgr.GetCombo() > 0)
        {
            ring.sprite =  rings[scoreMgr.GetComboIndex()];
            //pHealth.GetDamage(-scoreMgr.GetComboHeathAmount());
            //Debug.Log("피 이만큼 오르는중 " + scoreMgr.GetComboHeathAmount());
        }

        List<RaycastHit2D> hitList = new List<RaycastHit2D>();
        for(int i = 0; i <= moveTile; i++)
        {
            hitList.Add(Physics2D.Raycast(new Vector2(transform.position.x + i, transform.position.y - 5), Vector2.up, 10, LayerMask.GetMask("Tile")));
        }

        RaycastHit2D nowHit = hitList[0];
        RaycastHit2D nexthit = hitList[hitList.Count - 1];

        float movet = movetime;
        nextPosX = manager.moveCount + moveTile; //
        nextPosY = hitList[0].transform.position.y;
        //manager.moveCount += moveTile; //
        sources.volume = 0.5f;

        for (int i = 1; i <= moveTile; i++)
        {
            if ((i + 11) % moveTile == 0)
            {
                tileGenrator.FabOn(true, moveTile);
            }
            else
            {
                tileGenrator.FabOn(false, moveTile);
            }
        }

        if (transform.position.y < nextPosY)
        {
            initAni();
            animator.SetTrigger("Jump"); //jump
            //Debug.Log("Jump");
        }
        else if (transform.position.y > nextPosY) //다음ㅇ ㅔ이동할 y 밑 or 위에 있다면
        {
            initAni();
            animator.SetTrigger("Land"); 
        }
        else if (transform.position.y == nextPosY)
        {
            initAni();
            animator.SetTrigger("Run");

            if (isRight)
                animator.SetBool("IsRight", true); //run
            else
                animator.SetBool("IsRight", false); //run
            isRight = !isRight;
            //Debug.Log("Run");
        }

        List<float> yList = new List<float>();


        foreach (RaycastHit2D tile in hitList)
        {
            yList.Add(tile.transform.position.y);
        }


        for(int i = yList.Count - 1; i > 0; i--)
        {
            yList[i] -= yList[i - 1];
        }
        yList[0] -= transform.position.y;

        StartCoroutine(Move(1, transform.position.x, yList, hitList));
    }

    public IEnumerator Move(int i, float lastX, List<float> yList, List<RaycastHit2D> hitList)
    {
        /*foreach(float j in yList)
        {
            Debug.Log(j);
        }*/

        if(moveTile != 1)
        {
            isMove = true;
        }

        while (transform.position.x <= nextPosX && !pHealth.isDead)
        {
            cameraTf.position = new Vector3(transform.position.x + 2.5f, cameraTf.position.y, cameraTf.position.z);

            transform.Translate(Time.deltaTime * speed, Time.deltaTime * speed * yList[i], 0);

            if ((int)(lastX) != (int)(transform.position.x))
            {
                lastX = transform.position.x;
                if(i != yList.Count - 1)
                {
                    i++;
                    if(yList[i] > 0)
                    {
                        initAni();
                        animator.SetTrigger("Jump");
                    }
                    else if(yList[i] < 0)
                    {
                        initAni();
                        animator.SetTrigger("Land");
                    }
                    else
                    {
                        initAni();
                        animator.SetTrigger("Run");
                    }
                }
            }
            
            yield return null;
        }
        
        if(!pHealth.isDead)
        {
            transform.position = new Vector3(nextPosX, hitList[hitList.Count - 1].transform.position.y, transform.position.z);

            foreach (RaycastHit2D hit in hitList)
            {
                hit.collider.GetComponent<Tile>().SetBlack();
            }

            isMove = false;

            if (tileType != (int)hitList[1].collider.GetComponent<Tile>().tileType)
            {
                //피격
                sources.PlayOneShot(clips[3]);
                GetComponent<PlayerHealth>().GetDamage(damage);
                scoreMgr.SetCombo(0);
                ring.sprite = null;

                //Debug.Log(tileType + " " + (int)nexthit.collider.GetComponent<Tile>().tileType  + " " +"Get Hit!");
            }
            else
            {
                sources.PlayOneShot(clips[0]);
                GetComponent<PlayerHealth>().GetDamage(-damage);
                //Debug.Log(tileType + " " + (int)nexthit.collider.GetComponent<Tile>().tileType  + " " + "Clearrrrrrrrrr");
            }
        }

        //animator.SetTrigger("Idle"); //jump

        manager.moveCount += moveTile;
    }

    public void initAni()
    {
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Land");
    }
}
