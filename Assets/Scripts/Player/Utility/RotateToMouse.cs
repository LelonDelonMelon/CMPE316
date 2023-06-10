using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    private void Update()
    {
        
        
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to a world space direction
            Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;

            // Calculate the rotation based on the direction
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Set the rotation of the object
           // transform.rotation = rotation;
            
            transform.rotation = orientation.rotation;
        
    }

}
