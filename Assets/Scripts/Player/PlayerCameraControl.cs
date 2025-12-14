using UnityEngine;

public class PlayerCameraControl : MonoBehaviour {
    private Vector2 mouse;
    private const float Multiplier = 0.1f;
    public static Vector3 Position;
    public static Vector3 Forward;
    private float xRotation;
    private float yRotation;
    private void Update()
    {
        if (!GameManager.GameInProgress)
            return;
        mouse = InputWrapper.control.Player.Look.ReadValue<Vector2>();

        yRotation += mouse.x * Multiplier;
        xRotation -= mouse.y * Multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

        Position = transform.position;
        Forward = transform.forward;
    }
}