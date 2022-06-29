using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontReaper : MonoBehaviour
{
    public float speed;
    GameStageManager stageMg;

    public void OnEnable()
    {
        stageMg = FindObjectOfType<GameStageManager>();
        StartCoroutine(GoLeft());
    }

    IEnumerator GoLeft()
    {
        while (transform.position.x > -10)
        {
            if(stageMg.isBossStage)
            {
                Destroy(this.gameObject);
            }

            transform.Translate(Time.deltaTime * -speed, 0, 0);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
