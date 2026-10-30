using UnityEngine;
using System.Collections.Generic;
/*
AI use: unlike the straightforward bat tower which just rotates.
I had no clue where to start with tracking enemies and how it is 
implemented to handle radius of detection and how to then move
to the next enemy. I used the implementation given by AI to detect
the closest enemy in its radius to avoid having to store positional
values of how far a certain enemy was along the track and then to
decide which would be targetted next, a distance to nearest
object was a more understanable solution.
*/
public class TurretBehavior : MonoBehaviour
{
    [Header("Targetting Settings")]
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float scanInterval = 0.2f;

    [Header("Upgrade Settings")]
    public string towerName = "Turret";
    public int upgradeLevel = 1;
    public int upgradeCost = 100;
    public GoldCounter goldCounter;
    public BaseballDamage ballDam;

    [Header("Debug")]
    [SerializeField] private Transform currentTarget;

    [Header("Spawn Settings")]
    [Tooltip("The projectile or object prefab to instantiate.")]
    public GameObject projectilePrefab;

    [Tooltip("Where the projectile spawns from")]
    public Transform firePoint;

    [Tooltip("Time in seconds between fire")]
    public float fireInterval = 1.5f;

    [Header("Launch Settings")]
    [Tooltip("Speed at which the projectile is launched")]
    public float launchSpeed = 70.0f;

    [Tooltip("Automatically destroy the projectile after t seconds")]
    public float projectileLifetime = 3.0f;

    private float timer;
    private readonly List<string> enemyTags = new List<string> {"Dino", "TankDino", "SpeedDino"}; //valid tags again!
    private float scanTimer;

    void Start() {
        GameObject goldGO = GameObject.Find("Gold");
        goldCounter = goldGO.GetComponent<GoldCounter>(); //for future updating
        ballDam = projectilePrefab.GetComponent<BaseballDamage>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        scanTimer -= Time.deltaTime; //check each interval threshhold for a new target that may appear in the radius
        if (scanTimer <= 0f) {
            FindNearestTarget();
            scanTimer = scanInterval;
        }
        
        RotateTowardsTarget(); //when target exists it rotates so the tower faces it 
        if (currentTarget != null && timer >= fireInterval) { //check if target is in range primarily and if it is time to fire
            if (timer >= fireInterval) {
                FireProjectile(); 
                timer = 0f; //reset timer for updating fire rate
            }
        }
    }

    void FindNearestTarget() {
        Collider [] collidersInRange = Physics.OverlapSphere(transform.position, attackRadius); //if a collider is detected in the overlap sphere it gets marked 
        
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider col in collidersInRange) {
            if (enemyTags.Contains(col.tag)) { //calculates the nearest of the colliders in the radius for evaluating which enemy to target
                float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);

                if (distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = col.transform;
                }
            }
        }
        currentTarget = closestEnemy;
    }

    void RotateTowardsTarget () { // when target is determined, gets the transform to use for where the tower should rotate to
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0f; 

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }

    void FireProjectile() { //instantiates a baseball at the posiiton of the tower and applies force to it
        if (projectilePrefab == null || firePoint == null) {
            return;
        }
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.linearVelocity = firePoint.forward * launchSpeed;
        }
        if (projectileLifetime > 0f) { //if there is a lifetime set for the projectile, it will get deleted after that time
            Destroy(projectile, projectileLifetime);
        }
    }

    private void OnMouseDown() { //detects when the game object is clicked to reveal the upgrade menu
        TurretUpgradeUIManager.Instance.OpenUpgradeMenu(this);
    }

    public void Upgrade() { //when the upgrade is clicked can be afforded, it will upgrade the tower
        if (goldCounter.gold >= upgradeCost) {
            upgradeLevel++;
            ballDam.damageAmount += 6; //makes it faster and more powerful
            fireInterval -= 0.4f;
            goldCounter.gold -= upgradeCost; //update the amount of gold held and also the upgrade cost
            upgradeCost += 50;
        }
    }
}
