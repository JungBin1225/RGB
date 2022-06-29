using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameStageManager manager;

    public void OnEnable()
    {
        manager = GameObject.Find("StageManager").GetComponent<GameStageManager>();

        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.isBossStage)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            manager.gotCoin++;
            StartCoroutine(getCoin());
        }
    }

    private IEnumerator getCoin()
    {
        SpriteRenderer color = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        while(color.color.a > 0)
        {
            color.color = new Color(color.color.r, color.color.g, color.color.b, color.color.a - Time.deltaTime);
            yield return null;
        }

        Destroy(this.gameObject);
        //gameObject.SetActive(false);
    }
}
