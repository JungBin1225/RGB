using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGenerator : MonoBehaviour
{
    public GameObject frontReaperPrefab;
    GameStageManager stageMg;

    private void Start()
    {
        stageMg = FindObjectOfType<GameStageManager>();
        StartCoroutine(SpawnStart());

    }

    IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(10);

        while (!FindObjectOfType<ScoreMgr>().gameOver || !stageMg.isBossStage)
        {
            GameObject frontReaper = Instantiate(frontReaperPrefab) as GameObject;
            frontReaper.transform.position = new Vector3(FindObjectOfType<PlayerMove>().transform.position.x + 12, 0, 0);
            //for (int i = 0; i < frontReaper.transform.childCount; i++)
            //{
            //    frontReaper.transform.GetChild(i).position = new Vector3(0, frontReaper.transform.GetChild(i).position.y, 0);
            //}
            yield return new WaitForSeconds(Random.Range(8, 12));
        }
    }
}
