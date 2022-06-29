using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brtton : MonoBehaviour
{

    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void button_touch()
    {
        ani.SetBool("Clicked", true);
    }

    public void button_reaelesd()
    {
        ani.SetBool("Clicked", false);
    }
}
