using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    public float speed = 1;

    private float[] backgroundScales;
    private Transform cameraTransform;
    private float lastCameraX;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
            backgroundScales[i] = -(backgrounds[i].position.z);

        lastCameraX = cameraTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (lastCameraX - cameraTransform.position.x) * backgroundScales[i];
            Vector3 targetPosition = backgrounds[i].position + Vector3.right * parallax;
            Vector3 backgroundPosition = Vector3.Lerp(
                backgrounds[i].position,
                targetPosition,
                speed * Time.deltaTime
            );

            backgrounds[i].position = backgroundPosition;
        }

        lastCameraX = cameraTransform.position.x;
    }
}
