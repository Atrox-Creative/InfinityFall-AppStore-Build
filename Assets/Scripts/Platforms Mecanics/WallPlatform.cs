using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatform : MonoBehaviour
{
    private float timer;

    private void Awake()
    {
        transform.localScale = new Vector3(transform.localScale.x + 1, transform.localScale.y + 1.5f, transform.localScale.z + 1);
        transform.position = new Vector3(transform.position.x, transform.position.y + -0.03f, transform.position.z);
    }

    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= 1.2f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 0.6f);
            transform.localScale += new Vector3(40, 0, 40) * Time.deltaTime;
            if (timer >= 2.4f)
            {
                timer = 0;
            }
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 0.6f);
            transform.localScale += new Vector3(-40, 0, -40) * Time.deltaTime;

        }
    }
}
