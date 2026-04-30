using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(PlayerPowerUpController))]
public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;
    private AchievementManager achievementManager;
    public UnityEvent onPowerUpCollected;

    private void OnEnable()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager)
        {
            onPowerUpCollected.RemoveListener(achievementManager.HandlePowerUpCollected);
            onPowerUpCollected.AddListener(achievementManager.HandlePowerUpCollected);
        }
    }

    private void OnDisable()
    {
        if (achievementManager)
        {
            onPowerUpCollected.RemoveListener(achievementManager.HandlePowerUpCollected);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPowerUpController playerPowerUpController = collision.GetComponent<PlayerPowerUpController>();
        if (playerPowerUpController != null)
        {
            playerPowerUpController.ApplyPowerUp(powerUpData);
            onPowerUpCollected?.Invoke();
            // achievementManager.HandlePowerUpCollected();
            gameObject.SetActive(false); //will prob do pooling later on
        }

        // maybe here if I end up adding a collider at bottom of scene, then if collides I can set inactive (if using pooling)
    }
    // public float speed = 5f;
    // // Guard added to prevent stacking of the boost
    // private bool isBoosting = false;

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("PowerUp") && !isBoosting)
    //     {
    //         StartCoroutine(SpeedBoost());
    //         Destroy(other.gameObject);
    //     }
    // }

    // IEnumerator SpeedBoost()
    // {
    //     isBoosting = true;
    //     speed *= 2;
    //     Debug.Log("Speed Boost Activated: " + speed);

    //     yield return new WaitForSeconds(5f);

    //     speed /= 2;
    //     Debug.Log("Speed Boost Ended: " + speed);
    //     isBoosting = false;
    // }
}
// using System.Collections;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerPowerUp : MonoBehaviour
// {
//     public float speed = 5f;
//     private bool isBoosting = false;
//     private InputAction moveAction;

//     private void Start()
//     {
//         moveAction = InputSystem.actions.FindAction("Move");
//     }

//     void Update()
//     {
//         // if (Input.GetKeyDown(KeyCode.Space))
//         // {
//         //     StartCoroutine(SpeedBoost());
//         // }
//         // if ((moveAction != null) && (moveAction.ReadValue<Vector2>() != null))
//         // {
//         //     StartCoroutine(SpeedBoost());
//         // }
//         if (moveAction != null && moveAction.ReadValue<Vector2>() != Vector2.zero && !isBoosting)
//         {
//             StartCoroutine(SpeedBoost());
//         }
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("PowerUp"))
//         {
//             StartCoroutine(SpeedBoost());
//             Destroy(other.gameObject);
//         }
//     }

//     IEnumerator SpeedBoost()
//     {
//         isBoosting = true;
//         speed *= 2;
//         Debug.Log("Speed Boost Activated: " + speed);

//         yield return new WaitForSeconds(5f);

//         speed /= 2;
//         Debug.Log("Speed Boost Ended: " + speed);
//         isBoosting = false;
//     }
// }