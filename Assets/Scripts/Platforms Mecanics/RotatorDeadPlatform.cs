using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatorDeadPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    public float changeX;
    public float changeY;
    public float changeZ;
    private float rotationSpeed;
    

    private void Awake()
    {
        transform.localScale = new Vector3(transform.localScale.x + changeX, transform.localScale.y + changeY, transform.localScale.z + changeZ);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        rotationSpeed = Random.Range(1, 30);
    }
    void Update()
    {        
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
