using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNote : MonoBehaviour
{
    public string type;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition -= new Vector2(500 * Time.deltaTime, 0);

        if(rectTransform.anchoredPosition.x < -400)
        {
            Destroy(this.gameObject);
        }
    }
}
