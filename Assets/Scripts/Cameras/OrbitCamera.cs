using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Camera attachedCamera;
    public Transform target;
    public float minYAngle = 30f, maxYAngle = 90f;
    public float ySpeed = 120f, xSpeed = 120f;

    private void OnDrawGizmosSelected()
    {
        Vector3 cameraPos = attachedCamera.transform.position;
        Vector3 orbitPos = transform.position;
        
        float distance = Vector3.Distance(orbitPos, cameraPos);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cameraPos, orbitPos);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(orbitPos, distance);
    }

    void Start()
    {
        Rotate();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Rotate();
        }

        if (target)
        {
            transform.position = target.position;
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 euler = transform.eulerAngles;
        euler.x -= mouseY * ySpeed * Time.deltaTime;
        euler.y += mouseX * xSpeed * Time.deltaTime;
        euler.x = Mathf.Clamp(euler.x, minYAngle, maxYAngle);
        transform.eulerAngles = euler;
    }
}