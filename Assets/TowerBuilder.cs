using UnityEngine;
using TMPro;

/*
AI use: I had no clue where to start with the system to 
get towers to spawn from a ui and then selecting where to place it.
I used the implementation that ai provided me for the ui panel, buttons
and how to connect everything to have a working tower building script
This script specifically implements rays to see where the mouse
is currently over by finding where its hitting on a collider which i 
didnt even know could be used in this way.
A similar implementation is used for the tower upgrade ui's in terms of
setting up the buttons to be linked to a function script
*/

public class TowerBuilder : MonoBehaviour
{   
    public GoldCounter goldCounter; //for updating later
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject batPrefab; //prefabs for the towers
    [SerializeField] private GameObject turretPrefab;

    [Header("Placement Settings")]
    [SerializeField] private LayerMask placementLayer; //defines where the towers can be placed

    private GameObject towerToSpawn;
    private Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        mainCamera = Camera.main;
        GameObject goldGo = GameObject.Find("Gold"); //necessary references for updating
        goldCounter = goldGo.GetComponent<GoldCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (towerToSpawn != null) {
            HandlePlacement(); //checks to see if there is a given prefab to spawn
        }
    }

    public void SelectBat() {
        if (goldCounter.gold >= 25) {
            towerToSpawn = batPrefab; //handles the purchasing of the bat tower
            goldCounter.gold -= 25;
        }
        else {
            towerToSpawn = null;
        }
        
    }

    public void SelectTurret() {
        if (goldCounter.gold >= 40) {
            towerToSpawn = turretPrefab; //also handles purchasing but for turret
            goldCounter.gold -= 40;
        }
        else {
            towerToSpawn = null;
        }
    }

    private void HandlePlacement() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //creates a ray that detects the place where the mouse was clicked
        RaycastHit hit; //to see where on the map it should place the tower

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer)) { //checks if the ray hit the map from the cursor position
            if (Input.GetMouseButtonDown(0)) { //if in that valid position the mouse is clicked, it the corresponding tower prefab
                Instantiate(towerToSpawn, hit.point, Quaternion.identity); //to where the ray position hit the map
                towerToSpawn = null;
            }
        }
        
        if (Input.GetMouseButtonDown(1)) { //if the right click is pressed, it cancels the placement 
            towerToSpawn = null;
        }

    }
}
