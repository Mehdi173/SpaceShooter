using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform gun;
    [SerializeField] private BulletBehaviour bulletPrefab;
    [SerializeField] private float delayTime;
    
    private InputAction moveAction;
    private InputAction shootAction;
    private Vector2 axisValue;
    private Vector2 newVelocity;
    private Rigidbody2D rb2D;
    private float xVelocity;
    private float yVelocity;
    private float nextShotTime;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
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

    private void HandleShooting()
    {
        if (shootAction != null && shootAction.IsPressed() && Time.time > nextShotTime)
        {
            Instantiate(bulletPrefab, gun.position, gun.rotation);
            nextShotTime = Time.time + delayTime;
        }
    }
}
