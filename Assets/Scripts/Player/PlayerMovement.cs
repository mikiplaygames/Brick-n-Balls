using UnityEngine;
/// <summary>
/// Handles player movement based on input and camera direction.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform direction;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float acceleration = 12f;
    [SerializeField] float deceleration = 14f;
    Vector3 velocity;
    private void Update() {
        if (!GameManager.GameInProgress)
            return;
        Vector2 input = InputWrapper.control.Player.Move.ReadValue<Vector2>();
        Vector3 move = input.x * direction.right + input.y * direction.forward;
        
        Vector3 targetVel = move == Vector3.zero ? Vector3.zero : Mathf.Clamp01(input.magnitude) * maxSpeed * move.normalized;

        float rate = (targetVel.sqrMagnitude > 0f) ? acceleration : deceleration;
        velocity = Vector3.MoveTowards(velocity, targetVel, rate * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;
    }
}
