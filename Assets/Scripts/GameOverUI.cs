using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";
    [SerializeField] private SFXPlayer sfxPlayer;
    [SerializeField] private AudioClip deathExplosionClip;
    [SerializeField] private AudioClip gameOverClip;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        sfxPlayer.PlaySound(deathExplosionClip);
        StartCoroutine(PlayGameOverSoundAfterDelay());
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator PlayGameOverSoundAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        sfxPlayer.PlaySound(gameOverClip);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
