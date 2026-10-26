using UnityEngine;
using System.Collections.Generic;

public class TurretBehavior : MonoBehaviour
{
    [Header("Targetting Settings")]
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float scanInterval = 0.2f;

    [Header("Debug")]
    [SerializeField] private Transform currentTarget;

    [Header("Spawn Settings")]
    [Tooltip("The projectile or object prefab to instantiate.")]
    public GameObject projectilePrefab;

    [Tooltip("Where the projectile spawns from")]
    public Transform firePoint;

    [Tooltip("Time in seconds between fire")]
    public float fireInterval = 1.0f;

    [Header("Launch Settings")]
    [Tooltip("Speed at which the projectile is launched")]
    public float launchSpeed = 70.0f;

    [Tooltip("Automatically destroy the projectile after t seconds")]
    public float projectileLifetime = 3.0f;

    private float timer;
    private readonly List<string> enemyTags = new List<string> {"Dino", "TankDino", "SpeedDino"};
    private float scanTimer;

    void Update()
    {
        timer += Time.deltaTime;
        scanTimer -= Time.deltaTime;
        if (scanTimer <= 0f) {
            FindNearestTarget();
            scanTimer = scanInterval;
        }
        
        RotateTowardsTarget();
        if (currentTarget != null && timer >= fireInterval) {
            if (timer >= fireInterval) {
                FireProjectile();
                timer = 0f;
            }
        }
    }

    void FindNearestTarget() {
        Collider [] collidersInRange = Physics.OverlapSphere(transform.position, attackRadius);
        
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider col in collidersInRange) {
            if (enemyTags.Contains(col.tag)) {
                float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);

                if (distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = col.transform;
                }
            }
        }
        currentTarget = closestEnemy;
    }

    void RotateTowardsTarget () {
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }

    void FireProjectile() {
        if (projectilePrefab == null || firePoint == null) {
            return;
        }
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.linearVelocity = firePoint.forward * launchSpeed;
        }
        if (projectileLifetime > 0f) {
            Destroy(projectile, projectileLifetime);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
