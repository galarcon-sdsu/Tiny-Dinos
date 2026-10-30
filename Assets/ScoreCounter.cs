using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
Similar to the implementation for apple picker score counter
it even still has the name ScoreCounter and i think it would break
if i renamed it to GoldCounter so i will leave it alone
*/
public class GoldCounter : MonoBehaviour
{ 
    [Header("Dynamic")]

    public int gold = 100; //initial gold value to start

    private TextMeshProUGUI uiText; //define reference for uitext to update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>(); //get the ui instance that is on the scene
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = gold.ToString("#,0"); //take the gold value held and turn it into a string
    }
}
