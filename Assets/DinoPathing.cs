using UnityEngine;
/*
AI use: finding a way to control the pathing of the dinos confused me
I consulted ai and used its implementation of the checkpoint system
as well as the path building, tracking, movement, and rotation
*/
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

    [Header("Path complete SFX")]
    public AudioClip endFX;
    
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {

        GameObject healthGO = GameObject.Find("Health");
        healthCounter = healthGO.GetComponent<BaseHealth>(); //for updating base health
        currHealth = gameObject.GetComponent<DinoHealth>();

        switch(gameObject.tag) {
            case "TankDino":
            moveSpeed = 2f;
            break;

            case "Dino":
            moveSpeed = 5f; //like with dino health
            break;// find which dino instance is spawened and give it the corresponding move speed

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

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        MoveTowardsWaypoint();
        RotateTowardsWaypoint();
        CheckWaypointReached();
    }

    private void SetupPath() {
        if (pathHolder == null) {
            Debug.LogError($"Path Holder is missing on {gameObject.name}!"); //checks if the parent empty is missing, no path to setup
            return;
        }

        int childCount = pathHolder.childCount; //pathholder is the empty parent that holds 
        waypoints = new Transform[childCount];//the children empties whos transform's define the path checkpoint positions

        for (int i = 0; i < childCount; i++) {
            waypoints[i] = pathHolder.GetChild(i);
        }
                                    // get the empties and retrieve their positions to use them later
        if (waypoints.Length > 0) {
            transform.position = waypoints[0].position; 
            currentWaypointIndex = 1;
        }
    }

    private void MoveTowardsWaypoint() {
        if (currentWaypointIndex >= waypoints.Length) return; //each of these look for being the last checkpoint to call end of path function
        // if there is still distance to cover, using MoveTowards for smooth movement
        Vector3 targetPosition = waypoints[currentWaypointIndex].position; //checks for the position of the current checkpoint to reach
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsWaypoint() {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction); //the rotation position that the target needs to adjust to
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //Slerp for smooth 3d rotation
    }

    private void CheckWaypointReached() {
        if (currentWaypointIndex >= waypoints.Length) return;

        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);
        if (distanceToWaypoint <= waypointTolerance) { // if the tolerance is close enough to the destination checkpoint, update the target checkpoint
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length) {
                OnPathComplete();
            }
        }
    }

    private void OnPathComplete() {
        AudioSource.PlayClipAtPoint(endFX, transform.position); //play the enemy damage noise on the base
        healthCounter.health -= currHealth.currentHealth; //update base health accordingly with the amount of health that the dino had
        Destroy(this.gameObject); //destroy the dino asset
    }
}
