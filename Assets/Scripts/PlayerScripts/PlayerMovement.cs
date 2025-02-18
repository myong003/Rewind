using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 10f;
    private Rigidbody2D rb;

    private Vector2 movementInput;

    [SerializeField]
    private InputActionReference movement;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (movementInput.sqrMagnitude < 0.01f) {
            movementInput = Vector2.zero;
        }
        rb.linearVelocity = movementInput * movementSpeed;
    }
}
