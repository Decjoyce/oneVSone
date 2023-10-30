using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    TextMeshProUGUI text;
    float newAlpha = 1;
    bool bo;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (newAlpha >= 0.9f)
            bo = true;

        if (newAlpha <= 0.1f)
            bo = false;

        if (bo)
            newAlpha = Mathf.Lerp(text.color.a, 0, 5f * Time.deltaTime);
        else
            newAlpha = Mathf.Lerp(text.color.a, 1, 5f * Time.deltaTime);

        text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha); 
    }
}
