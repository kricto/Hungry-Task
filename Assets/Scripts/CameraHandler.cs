using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunction;

    private void Update()
    {
        Vector3 followPosition = GetCameraFollowPositionFunction();
        followPosition.z = transform.position.z;

        Vector3 moveDirection = (followPosition - transform.position).normalized;
        float distance = Vector3.Distance(followPosition, transform.position);
        float moveSpeed = 2f;

        if (distance > 0f)
        {
            Vector3 sumPart = moveDirection * distance; //Чтобы по y оси было видно лучше

            sumPart.x *= moveSpeed + 5f;
            sumPart.y *= moveSpeed + 5f;

            Vector3 newPosition = transform.position + sumPart * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newPosition, followPosition);

            if (distanceAfterMoving > distance)
            {
                newPosition = followPosition;
            }

            transform.position = newPosition;
        }
    }

    public void Setup(Func<Vector3> cameraFollowPositionFunction)
    {
        GetCameraFollowPositionFunction = cameraFollowPositionFunction;
    }
}
