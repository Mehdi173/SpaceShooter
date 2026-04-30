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

    // method for when achievement completes/unlocks
    // achievementQueue.enqueue(x);

    
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
        Debug.Log("HandleWaveReached called");
        CheckAchievements(AchievementType.ReachWave, currentWaveIndex);
    }

    public void HandlePowerUpCollected()
    {
        powerUpsCollected++;
        CheckAchievements(AchievementType.CollectPowerUps, powerUpsCollected);
    }

    // private void Start()
    // {
    //     StartCoroutine(ProcessAchievementQueue());
    // }

    private void CheckAchievements(AchievementType achievementType, int currentValue)
    {
        for (int i = 0; i < allAchievements.Length; i++)
        {
            currentAchievement = allAchievements[i];
            if ((currentAchievement.type == achievementType) && (!alreadyUnlockedAchievements.Contains(currentAchievement)))
            {
                if (currentValue >= currentAchievement.targetValue)
                {
                    // UnlockAchievement(allAchievements[i]);
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
        // isShowingAchievement = true;
        // // code to show achievement (e.g. UI animation)
        // yield return new WaitForSeconds(2f); // wait for 2 seconds before showing next achievement
        // isShowingAchievement = false;
        isProcessingQueue = true;
        while (achievementQueue.Count > 0)
        {
            // UnlockAchievement(achievementQueue.Dequeue());
            // // time in between (UI sutff happens before)
            // yield return new WaitForSeconds(3f);
            UnlockAchievement(achievementQueue.Dequeue());
            // time in between (UI sutff happens before)
            yield return new WaitForSeconds(3f);
            achievementPanel.SetActive(false);
            yield return new WaitForSeconds(1.5f);
        }
        isProcessingQueue = false;
        // while (true)
        // {
        //     if (achievementQueue.Count > 0) UnlockAchievement(achievementQueue.Dequeue());
        //     yield return new WaitForSeconds(5f);
        // }
        // for(; ;)
        // {
        //     if (achievementQueue.Count > 0) UnlockAchievement(achievementQueue.Dequeue());
        //     yield return new WaitForSeconds(5f);
        // }
    }

    private void UnlockAchievement(AchievementData achievement)
    {
        achievementText.text = "Achievement '" + achievement.achievementName + "' unlocked!";
        achievementPanel.SetActive(true);
        Debug.Log("Achievement unlocked");
        // UI stuff to show it
    }
}
