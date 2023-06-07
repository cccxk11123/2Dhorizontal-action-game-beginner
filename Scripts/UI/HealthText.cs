using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 upSpeed = new Vector3(0f, 75f, 0f);
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed = 0f;
    public float fadeTime = 0.6f;
    private Color startColor;

    private void Awake() 
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update() 
    {
        textTransform.position += upSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;
        if(timeElapsed < fadeTime)
        {
            float newAphla = startColor.a * (1 - (timeElapsed / fadeTime));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, newAphla);
        }
        else
        {
            Destroy(gameObject);
        }    
    }
}
