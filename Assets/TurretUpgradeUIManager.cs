using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
AI use: similar to the building manager, this instead defines the upgrade
for towers based on the buttons and panels 
The implementation for this script defines the upgrade panel
behavior for the turret tower and it is exactly the same
as the upgrade manager for the bat but it just gets
the script for the turret. there was probably a better way to
get the corresponding game object script
*/
public class TurretUpgradeUIManager : MonoBehaviour
{
    public static TurretUpgradeUIManager Instance {get; private set;}
    [Header("UI Elements")] 
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button closeButton;

    private TurretBehavior targetTower; //this is the only difference between the bat and turret ui managers

    private void Awake() {
        if (Instance == null) { //if there is no instance of ui then it self assigns
            Instance = this;
        }
        else {
            Destroy(gameObject); //if it exists, destroy for creating a new one when functions call for it
        }

        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        closeButton.onClick.AddListener(CloseUpgradeMenu); //listeners for the buttons to see when they are clicked
    }

    public void OpenUpgradeMenu(TurretBehavior tower) { //get the information for the instance of the tower clicked to see upgrade info
        targetTower = tower;

        if (targetTower.upgradeLevel <= 2) { //if not at max level, show appropopriate info
            nameText.text = $"{targetTower.towerName} (Lvl {targetTower.upgradeLevel})";
            costText.text = $"Cost: ${targetTower.upgradeCost}"; //updating the information
            upgradePanel.SetActive(true); //keep panel open
        }
        else {
            nameText.text = $"{targetTower.towerName} (MAX lvl)"; //when at max level, display appropriate labels 
            costText.text = $"N/A";
            upgradePanel.SetActive(true); //keep panel open
        }
        
    }
    
    private void OnUpgradeClicked() { // when the upgrade is button, call method to upgrade if applicable
        if (targetTower != null && targetTower.upgradeLevel <= 2) { //max level possible is 3
            targetTower.Upgrade();
            OpenUpgradeMenu(targetTower);
        }
    }

    public void CloseUpgradeMenu() { //when close button is closed, hide the upgrade menu
        upgradePanel.SetActive(false);
        targetTower = null;
    }
}
