using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player;

    public float speed = 0.5f;
    public float lookAheadFactor = 3;
    public float lookAheadThreshold = 0.1f;
    public float lookAheadReturnSpeed = 3;

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastPlayerPosition;
    private Vector3 lookAheadPosition;

    // Start is called before the first frame update
    private void Start()
    {
        lastPlayerPosition = player.position;
    }

    // Update is called once per frame
    private void Update()
    {
        float deltaX = player.position.x - lastPlayerPosition.x;
        if (Mathf.Abs(deltaX) > lookAheadThreshold)
        {
            lookAheadPosition = Vector3.right * lookAheadFactor * Mathf.Sign(deltaX);
        }


        // linear movement from lookAheadPosition to zero
        // e.g. Vector3 lookAheadPosition(1.8, 0, 0) to Vector3(0, 0, 0)
        float step = Time.deltaTime * lookAheadReturnSpeed;
        lookAheadPosition = Vector3.MoveTowards(lookAheadPosition, Vector3.zero, step);


        Vector3 lookAheadPlayerPosition = player.position + lookAheadPosition;
        Vector3 newPosition = Vector3.SmoothDamp(
            transform.position,
            lookAheadPlayerPosition,
            ref velocity,
            speed
        );
        newPosition.z = transform.position.z;

        transform.position = newPosition;
        lastPlayerPosition = player.position;
    }
}
