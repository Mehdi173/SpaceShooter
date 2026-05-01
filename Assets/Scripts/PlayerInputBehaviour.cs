using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform gun;
    [SerializeField] private BulletBehaviour bulletPrefab;
    [SerializeField] private float delayTime;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSoundClip;
    
    private InputAction moveAction;
    private InputAction shootAction;
    private Vector2 axisValue;
    private Vector2 newVelocity;
    private Rigidbody2D rb2D;
    private float xVelocity;
    private float yVelocity;
    private float nextShotTime;
    private float speedMultiplier = 1f;
    private float fireRateMultiplier = 1f;

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
            xVelocity = axisValue.x * speed * speedMultiplier;;
            newVelocity.x = xVelocity;
            yVelocity = axisValue.y * speed * speedMultiplier;
            newVelocity.y = yVelocity;
        }

        rb2D.linearVelocity = newVelocity;
    }

    private void HandleShooting()
    {
        if (shootAction != null && shootAction.IsPressed() && Time.time > nextShotTime)
        {
            BulletPool.Instance.SpawnBullet(gun.position, gun.rotation);
            audioSource.PlayOneShot(shootSoundClip);
            nextShotTime = Time.time + delayTime / fireRateMultiplier;
        }
    }

    public void SetSpeedMultiplier(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }

    public void SetFireRateMultiplier(float fireRateMultiplier)
    {
        this.fireRateMultiplier = fireRateMultiplier;
    }
}
