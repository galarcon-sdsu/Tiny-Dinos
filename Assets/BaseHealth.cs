using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    [Header("Dynamic")]
    public int health = 100; //total base health
    private TextMeshProUGUI uiText; //reference to the uitext which will update

    void Start() {
        uiText = GetComponent<TextMeshProUGUI>(); //get the uitext component for updating
    }

    // Update is called once per frame
    void Update() {
        uiText.text = health.ToString("#,0"); //turn the string value of the health into a number
        if (health <= 0) {
            SceneManager.LoadScene("SampleScene"); //when the base dies, restart the game
        }
    }
}
