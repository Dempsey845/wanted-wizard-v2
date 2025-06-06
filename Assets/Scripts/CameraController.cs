using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float smoothingAmount = 0.1f;
    [SerializeField] Transform playerBody;

    float xRotation;
    Vector2 lookInput;
    Vector2 currentLook;
    Vector2 smoothVelocity;

    InputAction lookAction;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        lookAction.Enable();

        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCancelled;
    }

    void OnLookCancelled(InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
    }

    void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        // Sync camera position to player's camera holder (ensures smooth follow)
        transform.position = playerBody.position;

        // Look input smoothing
        Vector2 target = mouseSensitivity * lookInput;
        currentLook = Vector2.SmoothDamp(currentLook, target, ref smoothVelocity, smoothingAmount);

        // Vertical rotation (pitch)
        xRotation -= currentLook.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (yaw) on body
        playerBody.Rotate(Vector3.up * currentLook.x);
    }

}
