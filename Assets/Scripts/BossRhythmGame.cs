using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRhythmGame : MonoBehaviour
{
    public List<GameObject> noteList;
    public float spawnTime;
    private float time;

    void OnEnable()
    {
        time = spawnTime;
    }


    void Update()
    {
        time -= Time.deltaTime;

        if(time < 0)
        {
            int index = Random.Range(0, 3);
            GameObject note = Instantiate(noteList[index], transform);

            time = spawnTime;
        }
    }
}
