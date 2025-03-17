using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player; 
    public float offsetX = 2f;
    public float offsetY = 1f; 
    public float smoothSpeed = 0.125f; 

    void LateUpdate() 
    {
        if (Player != null) 
        {
            Vector3 targetPosition = new Vector3(
                Player.transform.position.x + offsetX,
                Player.transform.position.y + offsetY,
                transform.position.z 
            );

          
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}