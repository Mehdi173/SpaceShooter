using UnityEngine;

// [RequireComponent(typeof(PlayerPowerUpController))]
public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPowerUpController playerPowerUpController = collision.GetComponent<PlayerPowerUpController>();
        if (playerPowerUpController != null)
        {
            playerPowerUpController.ApplyPowerUp(powerUpData);
            gameObject.SetActive(false); //will prob do pooling later on
        }
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