using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    
    [SerializeField]
    private float cameraMovementSpeed;

    // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 desiredCameraPosition = player.position;
            Transform mainCamera = transform;

            Vector3 currentCameraPosition = mainCamera.position;
            desiredCameraPosition.z = currentCameraPosition.z;

            Vector3 cameraMoveDirection = (desiredCameraPosition - currentCameraPosition).normalized;
            float distance = Vector3.Distance(desiredCameraPosition,currentCameraPosition);

            currentCameraPosition = transform.position + cameraMoveDirection * (distance * cameraMovementSpeed * Time.deltaTime);
            mainCamera.position = currentCameraPosition;
        }
}

