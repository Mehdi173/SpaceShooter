using UnityEngine;
using TMPro;
using System.Collections;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemyTypesText;
    [SerializeField] private GameObject newWaveAnnouncementPanel;
    [SerializeField] private TMP_Text newWaveAnnouncementPanelText;

    public void UpdateUIWaveDetails(int currentWaveIndex, string enemyTypes)
    {
        // waveText = "Wave: " + currentWaveIndex;
        // waveText.SetText("Wave: " + currentWaveIndex);
        waveText.SetText("Wave {0}", currentWaveIndex);
        enemyTypesText.text = "Enemies: " + enemyTypes;
    }

    public IEnumerator ShowNewWaveAnnouncement(int newWaveNumber, string enemyTypes)
    {
        yield return StartCoroutine(HandleNewWaveAnnouncement(newWaveNumber, enemyTypes));
    }

    private IEnumerator HandleNewWaveAnnouncement(int newWaveNumber, string enemyTypes)
    {
        newWaveAnnouncementPanelText.text = "Wave " + newWaveNumber + " incoming!\n\nType of enemies: " + enemyTypes;
        // newWaveAnnouncementPanelText.SetText("Wave {0} incoming...\nType of enemies: {1}", newWaveNumber, enemyTypes);
        newWaveAnnouncementPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        newWaveAnnouncementPanel.SetActive(false);
    }

    public void TogglePause()
    {
        // timescale change + menu?, maybe toggle doesn't work then?
    }
}
