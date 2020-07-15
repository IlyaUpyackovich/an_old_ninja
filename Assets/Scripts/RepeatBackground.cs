using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;
    private int leftIndex;
    private int rightIndex;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        layers = new Transform[transform.childCount];
        for (int i = 0; i < layers.Length; i++)
            layers[i] = transform.GetChild(i);

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (cameraTransform.position.x < layers[leftIndex].transform.position.x + 10)
        {
            MoveRightToLeft();
        }

        if (cameraTransform.position.x > layers[rightIndex].transform.position.x - 10)
        {
            MoveLeftToRight();
        }
    }

    void MoveLeftToRight()
    {
        float positionX = layers[rightIndex].position.x + backgroundSize;
        layers[leftIndex].position = new Vector2(positionX, layers[leftIndex].position.y);

        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }

    }

    void MoveRightToLeft()
    {
        float positionX = layers[leftIndex].position.x - backgroundSize;
        layers[rightIndex].position = new Vector2(positionX, layers[rightIndex].position.y);

        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }
}
