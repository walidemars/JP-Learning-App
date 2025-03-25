using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private InputAction moveAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void Update()
    {
        Vector2 readMovement = moveAction.ReadValue<Vector2>();

        Vector3 movement = new Vector3(readMovement.x, readMovement.y, 0);
        movement.Normalize();
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
