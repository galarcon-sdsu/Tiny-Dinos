using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{ 
    [Header("Dynamic")]

    public int gold = 0;

    private TextMeshProUGUI uiText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = gold.ToString("#,0");
    }
}
