using UnityEngine;

public class DinoPathing : MonoBehaviour
{
    [Header("Path Settings")]
    private Transform[] waypoints;
    private float moveSpeed = 5f;
    private float rotationSpeed = 5f;
    private float arrivalThreshhold = 0.2f;
    private bool loopPath = false;

    private int currentWayPointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (waypoints.Length > 0) {
            transform.position = waypoints[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
