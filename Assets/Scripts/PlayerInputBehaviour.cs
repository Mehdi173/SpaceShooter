using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private InputAction moveAction;
    private Vector2 axisValue;
    private Vector2 newVelocity;
    private Rigidbody2D rb2D;
    private float xVelocity;
    private float yVelocity;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        newVelocity = rb2D.linearVelocity;

        if (moveAction != null)
        {
            axisValue = moveAction.ReadValue<Vector2>();
            xVelocity = axisValue.x * speed;
            newVelocity.x = xVelocity;
            yVelocity = axisValue.y * speed;
            newVelocity.y = yVelocity;
        }

        rb2D.linearVelocity = newVelocity;
    }
}
