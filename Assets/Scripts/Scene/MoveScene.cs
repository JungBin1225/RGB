using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public int toMoveSceneIndex;
    public bool canMoveSceneByAnyKey;

    public void MoveToAtherScene()
    {
        SceneManager.LoadScene(toMoveSceneIndex);
    }

    public void Update()
    {
        if(canMoveSceneByAnyKey)
        {
            //Debug.Log("aa");
            if(Input.anyKeyDown)
            {
                MoveToAtherScene();
            }
        }
    }

}
