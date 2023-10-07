using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Min(0.0001f)] private float playerSpeed = 5f;
    [SerializeField] private float cameraSensitivity = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Min(0.0001f)] private float cameraRotationClamp = 90;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnValidate()
    {
        if (!cameraTransform && Camera.main)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.localRotation *= Quaternion.Euler(0, mouseX * cameraSensitivity, 0);
        cameraTransform.localRotation = Quaternion.Euler(ClampAngle(-mouseY * cameraSensitivity + cameraTransform.localEulerAngles.x, cameraRotationClamp), 0, 0); 
    }

    static float ClampAngle(float angle, float angleClamp)
    {
        return (angle < angleClamp && angle > 360 - angleClamp
            ? (angle > 360 - angleClamp ? 360 - angleClamp : angleClamp)
            : angle);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = transform.rotation * new Vector3(horizontal, 0, 
            vertical);
        moveDirection = moveDirection.magnitude > 1 ? moveDirection.normalized : moveDirection;
        
        transform.position += moveDirection * (playerSpeed * Time.fixedDeltaTime);
    }
}
