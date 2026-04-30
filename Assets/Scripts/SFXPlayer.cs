using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip) audioSource?.PlayOneShot(audioClip);
    }
}
