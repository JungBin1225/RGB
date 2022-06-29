using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    //이미지를 받는 배열
    public GameObject[] arts;
    int sceneCount = 0;

    void Start()
    {
        StartCoroutine(EnableScene());
    }
    IEnumerator EnableScene()
    {
        while (sceneCount < arts.Length)
        {
            OnArt();
            yield return new WaitForSeconds(2);
        }
        FindObjectOfType<MoveScene>().MoveToAtherScene();
    }

    public void OnArt()
    {
        if(sceneCount>0)
        arts[sceneCount-1].SetActive(false);
        arts[sceneCount].SetActive(true);
        sceneCount++;
    }

}
