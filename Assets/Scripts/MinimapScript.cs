using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        
        Vector3 newPosition = player.position;
        newPosition.z = -200;
        transform.position = newPosition;

    }
}
