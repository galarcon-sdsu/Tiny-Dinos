using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Set the rotation speed for the X, Y, and Z axes in degrees per second.")]
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f);

    [Header("Upgrade Settings")]
    public string towerName = "Bat";
    public int upgradeLevel = 1;
    public int upgradeCost = 75;
    public GoldCounter goldCounter;
    public BatDamage batDam;

    // Update is called once per frame

    void Start() {
        GameObject goldGO = GameObject.Find("Gold");

        goldCounter = goldGO.GetComponent<GoldCounter>();
        
        batDam = gameObject.GetComponent<BatDamage>();
    }
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);

    }

    private void OnMouseDown() {
        UpgradeUIManager.Instance.OpenUpgradeMenu(this);
    }

    public void Upgrade() {
        if (goldCounter.gold >= upgradeCost) {
            upgradeLevel++;
            upgradeCost += 25;
            batDam.damageAmount += 15;

            goldCounter.gold -= upgradeCost;
            Debug.Log("Upgraded bat tower");
        }
    }

}
