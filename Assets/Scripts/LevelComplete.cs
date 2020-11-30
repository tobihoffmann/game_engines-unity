
using Managers;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    /// <summary>
    /// If the player walks on the trigger, the level completes.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.ChangeLevel();
    }
}
