using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public Ball targetBall; // Target ball selected (which is generally the Cue ball
    public Transform stick;
    public GameObject model;
    public float minPower = 0f; // The min power which maps to the distance
    public float maxPower = 20f;// The max power which maps to the distance
    public float maxDistance = 5f; //The maximum distance in units the cue can be dragged back

    private float hitPower; // The final calculated hit power to fire the ball
    private Vector3 prevMousePos; // The mouse position obtained when left-clicking
    private Ray mouseRay; // The ray of the mouse

    // Helps visualize the mouse ray and direction of fire
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(targetBall.transform.position, targetBall.transform.position + stick.forward * hitPower);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetHitPoint(), .1f);
    }

    // Rotates the cue to wherever the mouse is pointing (using Raycast)
    void Aim()
    {
        // Obtain direction from the cue's position to the raycast's hit point
        Vector3 dir = stick.transform.position - GetHitPoint();
        // Convert direction to angle in degrees
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        // Rotate towards that angle
        stick.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        // Position cue to the ball's position
        stick.position = targetBall.transform.position;
    }

    Vector3 GetHitPoint()
    {
        // Calculate mouse ray before performing raycast
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Raycast Hit container for the hit information
        RaycastHit hit;
        // Perform the Raycast
        if (Physics.Raycast(mouseRay, out hit))
        {
            // Obtain direction from the cue's position to the raycast's hit point
            return hit.point;
        }

        return Vector3.zero;
    }

    // Deactivates the Cue
    public void Deactivate()
    {
        model.SetActive(false);
    }

    // Activates the Cue
    public void Activate()
    {
        model.SetActive(true);
    }

    // Allows you to drag the cue back and calculates power dealt to the ball
    void Drag()
    {
        // Get mouse hit pos
        Vector3 currMousePos = GetHitPoint();
        // Calculate distance from previous mouse position to the current mouse position
        float distance = Vector3.Distance(prevMousePos, currMousePos);
        // Clamp the distance between 0 - maxDistance
        distance = Mathf.Clamp(distance, 0, maxDistance);
        // Calculate a percentage for the distance
        float distPercentage = distance / maxDistance;
        // Use percentage of distance to map to the minPower - maxPower values
        hitPower = Mathf.Lerp(minPower, maxPower, distPercentage);
        // Store target ball's position in smaller variable
        Vector3 targetPos = targetBall.transform.position;
        // Position the cue back using distance
        stick.position = targetPos + -model.transform.up * distance;
    }

    // Fires off the ball
    void Fire()
    {
        // Hit the ball with direction and power
        targetBall.Hit(stick.forward, hitPower);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBall.IsStopped())
        {
            Activate();
        }
        else
        {
            Deactivate();
        }

        // Check if left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Store the click position as the 'prevMousePos'
            prevMousePos = GetHitPoint();
        }

        // Check if left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Perform drag mechanic
            Drag();
        }
        else
        {
            // Perform aim mechanic
            Aim();
        }

        // Check if the left mouse button is up
        if (Input.GetMouseButtonUp(0))
        {
            // Hit the ball
            Fire();
        }
    }
}
