using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Set the rotation speed for the X, Y, and Z axes in degrees per second.")]
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
