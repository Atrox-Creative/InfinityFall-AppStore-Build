using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc_Rotator : MonoBehaviour
{
    private Vector3 _rotation;
    public float rotatorSpeed;

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) && GameManager.singleton.isGameOver == false || Input.GetKey("left") && GameManager.singleton.isGameOver == false)
        {
            _rotation = Vector3.up;
        }
        else if(Input.GetKey(KeyCode.D) && GameManager.singleton.isGameOver == false || Input.GetKey("right") && GameManager.singleton.isGameOver == false)
        {
            _rotation = Vector3.down;
        }
        else
        {
            _rotation = Vector3.zero;
        }

        transform.Rotate(_rotation * rotatorSpeed * Time.deltaTime);
    }
}
