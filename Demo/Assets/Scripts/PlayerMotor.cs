using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 Velocity = Vector3.zero;
    private Vector3 Rotation = Vector3.zero;
    private Vector3 CameraRotation = Vector3.zero;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Move(Vector3 velocity)
    {
        Velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        Rotation = rotation;
    }

    public void RotateCamera(Vector3 cameraRotation)
    {
        CameraRotation = cameraRotation;
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();

    }

    void PerformMovement()
    {
        if (Velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + Velocity * Time.fixedDeltaTime);

        }

    }

    void PerformRotation()
    {

        rb.MoveRotation(rb.rotation * Quaternion.Euler (Rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-CameraRotation);
        }

    }


}

