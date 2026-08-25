using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotatorSpeed;

    private void FixedUpdate()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && GameManager.singleton.isGameOver == false)
        {
            Vector3 Rotation = Input.GetTouch(0).deltaPosition;
            transform.Rotate(0, Rotation.x * -rotatorSpeed * Time.deltaTime, 0);
        }
    }
}
