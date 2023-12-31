using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowMovingHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float arrowAngle;
    [SerializeField] private float arrowRotationSpeed;
    [SerializeField] private float rotationDelta;
    [SerializeField] private Transform initialPosition;

    private float[] arrowSpeeds = new[] { 8f, 7f, 6f, 5f };
    private float arrowSpeed;
    private bool isActive;

    public void Restart()
    {
        StopAllCoroutines();
        rigidBody.constraints = RigidbodyConstraints2D.None;
        transform.position = initialPosition.position;
        var angles = transform.rotation.eulerAngles;
        angles.z = 90;
        
        transform.rotation = Quaternion.Euler(angles);
    }

    public void StartMove()
    {
        isActive = true;
        SetPlayerInitialSpeed();
    }

    public void StopMove()
    {
        isActive = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidBody.velocity = Vector2.zero;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        
        var edge = other.GetComponent<GameEdge>();
        
        if (edge != null)
        {
            rigidBody.velocity = Vector2.Reflect(rigidBody.velocity, Vector2.right);
            StopAllCoroutines();
            StartCoroutine(RotateArrow(rigidBody.velocity.normalized));
        }
    }

    private void SetPlayerInitialSpeed()
    {
        PlayerSavesLoad.Load();
        arrowSpeed = arrowSpeeds[PlayerSavesLoad.playerMaxSpeedPoints];
        
        var cos1 = Mathf.Cos(arrowAngle);
        var f1 = -Mathf.Sin(arrowAngle);
        var sin = Mathf.Sin(arrowAngle);
        var cos = Mathf.Cos(arrowAngle);
		
        var destinationVector = new Vector2(cos1 * transform.right.x + f1 * transform.right.y, sin * transform.right.x + cos * transform.right.y);
        Debug.Log(Vector2.Angle(transform.right, destinationVector));
        
        rigidBody.velocity = destinationVector * arrowSpeed;
        StartCoroutine(RotateArrow(destinationVector.normalized));
    }
    
    private IEnumerator RotateArrow(Vector3 destination)
    {
        var crossMultiply = Vector3.Cross(transform.right, destination);
        int rotationDirection = (int)(crossMultiply.z / Mathf.Abs(crossMultiply.z));
        var angle = Mathf.Rad2Deg * Mathf.Asin(crossMultiply.magnitude);
        
        while (angle > 0)
        {
            if (!isActive) yield break;
            
            var angles = transform.rotation.eulerAngles;
            crossMultiply = Vector3.Cross(transform.right, destination);
            rotationDirection = (int)(crossMultiply.z / Mathf.Abs(crossMultiply.z));
            angle = Mathf.Asin(crossMultiply.magnitude);
            
            angles.z += rotationDirection * arrowRotationSpeed * (Mathf.Abs(angle) + rotationDelta);
            transform.rotation = Quaternion.Euler(angles);
            yield return new WaitForFixedUpdate();
        }

        transform.right = destination;
    }
}
