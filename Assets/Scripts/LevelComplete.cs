
using Managers;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    private GameObject levelChanger;

    private int currentLevel;

    /// <summary>
    /// If the player walks on the trigger, the level completes.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        levelChanger.GetComponent<LevelChanger>();
        currentLevel = GameManager.Instance.GetCurrentLevelIndex();
        if (other.CompareTag("Player"))
        {
            if (currentLevel >= GameManager.Instance.GetSceneCount())
                currentLevel = -1;
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(currentLevel + 1);
        }
    }
}
