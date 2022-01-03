using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float cameraFollowSpeed = 5f;

    Vector3 distance;

    void Start()
    {
        distance = Calculate(target);

    }

    void FixedUpdate()
    {
        if (target != null)
        {
            MoveTheCamera();
        }
    }

    void MoveTheCamera()
    {
        Vector3 targetToMove = target.position + distance;
        transform.position = Vector3.Lerp(transform.position, targetToMove, cameraFollowSpeed * Time.deltaTime);
        transform.LookAt(target.transform.position);
    }


    Vector3 Calculate(Transform newTarget)
    {
        return transform.position - newTarget.position;
    }
}
