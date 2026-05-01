using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private AchievementData[] allAchievements;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private TMP_Text achievementText;
    private Queue<AchievementData> achievementQueue = new Queue<AchievementData>();
    private HashSet<AchievementData> alreadyUnlockedAchievements = new HashSet<AchievementData>();
    private bool isProcessingQueue = false;
    private int enemiesKilled = 0;
    private int powerUpsCollected = 0;
    private AchievementData currentAchievement;

    
    public void TestMethod()
    {
        Debug.Log("Test");
    }
    
    public void HandleEnemyKilled()
    {
        enemiesKilled++;
        CheckAchievements(AchievementType.KillEnemies, enemiesKilled);
    }

    public void HandleWaveReached(int currentWaveIndex)
    {
        CheckAchievements(AchievementType.ReachWave, currentWaveIndex);
    }

    public void HandlePowerUpCollected()
    {
        powerUpsCollected++;
        CheckAchievements(AchievementType.CollectPowerUps, powerUpsCollected);
    }

    private void CheckAchievements(AchievementType achievementType, int currentValue)
    {
        for (int i = 0; i < allAchievements.Length; i++)
        {
            currentAchievement = allAchievements[i];
            if ((currentAchievement.type == achievementType) && (!alreadyUnlockedAchievements.Contains(currentAchievement)))
            {
                if (currentValue >= currentAchievement.targetValue)
                {
                    alreadyUnlockedAchievements.Add(currentAchievement);
                    achievementQueue.Enqueue(currentAchievement);

                    if (!isProcessingQueue)
                    {
                        StartCoroutine(ProcessAchievementQueue());
                    }
                }
            }
        }
    }

    private IEnumerator ProcessAchievementQueue()
    {
        isProcessingQueue = true;
        while (achievementQueue.Count > 0)
        {
            UnlockAchievement(achievementQueue.Dequeue());
            yield return new WaitForSeconds(3f);
            achievementPanel.SetActive(false);
            yield return new WaitForSeconds(1.5f);
        }
        isProcessingQueue = false;
    }

    private void UnlockAchievement(AchievementData achievement)
    {
        achievementText.text = "Achievement '" + achievement.achievementName + "' unlocked!";
        achievementPanel.SetActive(true);
    }
}
