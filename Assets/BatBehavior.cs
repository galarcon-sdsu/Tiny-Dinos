using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Set the rotation speed for the X, Y, and Z axes in degrees per second.")]
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f); 

    [Header("Upgrade Settings")]
    public string towerName = "Bat"; //variables for upgrade for upgrade panel
    public int upgradeLevel = 1;
    public int upgradeCost = 75;
    public GoldCounter goldCounter; //reference for gold counter
    public BatDamage batDam; //reference for accessing bat damage

    void Start() {
        GameObject goldGO = GameObject.Find("Gold");
        goldCounter = goldGO.GetComponent<GoldCounter>(); //getting components for updating
        batDam = gameObject.GetComponent<BatDamage>();
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime); //keep rotating the bat
    }

    private void OnMouseDown() {
        UpgradeUIManager.Instance.OpenUpgradeMenu(this); //open ui when the tower detects it is clicked on
    }

    public void Upgrade() { //upgrade function for this tower instance
        if (goldCounter.gold >= upgradeCost) { //make sure it can be afforded
            upgradeLevel++; 
            batDam.damageAmount += 10; //adjust this instance damage
            goldCounter.gold -= upgradeCost; //update the goldcounter
            upgradeCost += 25;
        }
    }

}
