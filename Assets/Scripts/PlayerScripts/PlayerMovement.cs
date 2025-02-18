using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 10f;
    private Rigidbody2D rb;

    private Vector2 movementInput;

    private float inputThreshold = 0.01f;

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
        if (movementInput.sqrMagnitude < inputThreshold) 
        {
            movementInput = Vector2.zero;
        }

        rb.linearVelocity = movementInput.normalized * movementSpeed;

        if (movementInput.x < -inputThreshold)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } 
        else if (movementInput.x > inputThreshold) 
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
