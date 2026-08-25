using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    private float rotationSpeed;


    private void Awake()
    {
        rotationSpeed = Random.Range(1, 8);
    }
    void Update()
    {
        transform.parent.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
