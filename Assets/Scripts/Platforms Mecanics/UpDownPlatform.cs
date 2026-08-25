using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    private float timer;
    private float rotationSpeed;

    private void Awake()
    {
        rotationSpeed = Random.Range(1, 5);
    }

    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= 1.2f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * rotationSpeed);
            if (timer >= 2.4f)
            {
                timer = 0;
            }
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * rotationSpeed);

        }
    }
}
