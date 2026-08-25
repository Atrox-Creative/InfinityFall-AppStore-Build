using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Ball ball;    

    private float Offset;
    private bool hasCollided = false;
    public enum cameraState { Following, Static };
    public cameraState state = cameraState.Following;

    void Awake()
    {
        Offset = transform.position.y - ball.transform.position.y;
        ball.onCollide = OnBallCollide;
    }

    private void Update()
    {
        if (state == cameraState.Static)
        {

            float currentOffset = transform.position.y - ball.transform.position.y;
            if (currentOffset > Offset + 0.1f)
            {
                state = cameraState.Following;
            }
        } else if (hasCollided)
        {
            state = cameraState.Static;
            hasCollided = false;
        }

        if (state == cameraState.Following) Follow();
    }

    void Follow()
    {
        Vector3 curPos = transform.position;
        curPos.y = ball.transform.position.y + Offset;

        transform.position = curPos;
    }

    void OnBallCollide()
    {
        hasCollided = true;
    }
}
