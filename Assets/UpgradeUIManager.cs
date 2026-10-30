using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
refer to turret upgrade ui manager, it is the same
*/
public class UpgradeUIManager : MonoBehaviour
{
    public static UpgradeUIManager Instance {get; private set;}
    [Header("UI Elements")] 
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button closeButton;

    private BatBehavior targetTower;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        closeButton.onClick.AddListener(CloseUpgradeMenu);
    }

    public void OpenUpgradeMenu(BatBehavior tower) {
        targetTower = tower;

        if (targetTower.upgradeLevel <= 2) {
            nameText.text = $"{targetTower.towerName} (Lvl {targetTower.upgradeLevel})";
            costText.text = $"Cost: ${targetTower.upgradeCost}";
            upgradePanel.SetActive(true);
        }
        else {
            nameText.text = $"{targetTower.towerName} (MAX lvl)";
            costText.text = $"N/A";
            upgradePanel.SetActive(true);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnUpgradeClicked() {
        if (targetTower != null && targetTower.upgradeLevel <= 2) {
            targetTower.Upgrade();

            OpenUpgradeMenu(targetTower);
        }
    }

    public void CloseUpgradeMenu() {
        upgradePanel.SetActive(false);
        targetTower = null;
    }
}
