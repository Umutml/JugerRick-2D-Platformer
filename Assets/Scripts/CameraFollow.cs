using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float smoothFollow;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        
    }

    void Update()
    {
       
    }
    private void LateUpdate()
    {
        Vector3 playerPos = playerTransform.position + offset;
        Vector3 followPlayer = Vector3.Lerp(transform.position, playerPos, smoothFollow);
        transform.position = followPlayer;
    }
}
