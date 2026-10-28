using UnityEngine;

public class DinoPathing : MonoBehaviour
{
    [Header("Path Settings")]
    private float moveSpeed;
    public BaseHealth healthCounter;
    public DinoHealth currHealth;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float waypointTolerance = 0.1f;

    [Header("Path Seteup")]
    [SerializeField] private Transform pathHolder;
    
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject healthGO = GameObject.Find("Health");
        healthCounter = healthGO.GetComponent<BaseHealth>();
        currHealth = gameObject.GetComponent<DinoHealth>();
        switch(gameObject.tag) {
            case "TankDino":
            moveSpeed = 2f;
            break;

            case "Dino":
            moveSpeed = 5f;
            break;

            case "SpeedDino":
            moveSpeed = 10f;
            break;

            default:
            Debug.LogWarning($"Tag {gameObject.tag} not recognized.");
            moveSpeed = 5f;
            break;
        }
        SetupPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        MoveTowardsWaypoint();
        RotateTowardsWaypoint();
        CheckWaypointReached();
    }

    private void SetupPath() {
        if (pathHolder == null) {
            Debug.LogError($"Path Holder is missing on {gameObject.name}!");
            return;
        }

        int childCount = pathHolder.childCount;
        waypoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++) {
            waypoints[i] = pathHolder.GetChild(i);
        }

        if (waypoints.Length > 0) {
            transform.position = waypoints[0].position;
            currentWaypointIndex = 1;
        }
    }

    private void MoveTowardsWaypoint() {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsWaypoint() {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void CheckWaypointReached() {
        if (currentWaypointIndex >= waypoints.Length) return;

        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

        if (distanceToWaypoint <= waypointTolerance) {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length) {
                OnPathComplete();
            }
        }
    }

    private void OnPathComplete() {
        Debug.Log($"{gameObject.name} reached the end of the path");
        
        healthCounter.health -= currHealth.currentHealth;
        Destroy(this.gameObject);
    }
}
