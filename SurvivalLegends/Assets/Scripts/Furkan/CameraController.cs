using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTrans;
    [SerializeField] float turnSpeed = 2f;

    void LateUpdate()
    {
        if (followTrans != null)
        {
            transform.position = followTrans.position;
        }
    }

    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * turnSpeed);
    }
}
