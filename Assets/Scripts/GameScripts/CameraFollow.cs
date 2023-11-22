using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float offset;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float delta;
 
    private void FixedUpdate()
    {
        var cameraPosition = transform.position;
        var distance = targetTransform.position.y + offset - transform.position.y;
        var magnitude = Mathf.Abs(distance);
        
        if (distance > 0)
        {
            cameraPosition.y += cameraSpeed * (magnitude + delta);
            transform.position = cameraPosition;
        }

        if (distance < 0)
        {
            cameraPosition.y -= cameraSpeed * (magnitude + delta);
            transform.position = cameraPosition;
        }
    }

}
