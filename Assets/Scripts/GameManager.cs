using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public string selected_Char;

    void Awake() //¾ÀÀÌ ¹Ù²î¾îµµ ÆÄ±«µÇÁö ¾ÊÀ½
    {
        if (gameManager == null)
            gameManager = this;

        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        selected_Char = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
