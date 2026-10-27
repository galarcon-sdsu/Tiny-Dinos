using UnityEngine;
using TMPro;

public class TowerBuilder : MonoBehaviour
{   
    public GoldCounter goldCounter;
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private GameObject turretPrefab;

    [Header("Placement Settings")]
    [SerializeField] private LayerMask placementLayer;

    private GameObject towerToSpawn;
    private Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        mainCamera = Camera.main;
        GameObject goldGo = GameObject.Find("Gold");
        goldCounter = goldGo.GetComponent<GoldCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (towerToSpawn != null) {
            HandlePlacement();
        }
    }

    public void SelectBat() {
        if (goldCounter.gold >= 25) {
            towerToSpawn = batPrefab;
            goldCounter.gold -= 25;
            Debug.Log("Bat selected");
        }
        else {
            towerToSpawn = null;
        }
        
    }

    public void SelectTurret() {
        if (goldCounter.gold >= 40) {
            towerToSpawn = turretPrefab;
            goldCounter.gold -= 40;
            Debug.Log("Turret selected");
        }
        else {
            towerToSpawn = null;
        }
    }

    private void HandlePlacement() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer)) {
            if (Input.GetMouseButtonDown(0)) {
                Instantiate(towerToSpawn, hit.point, Quaternion.identity);
                towerToSpawn = null;
            }
        }
        
        if (Input.GetMouseButtonDown(1)) {
            towerToSpawn = null;
        }

    }
}
