
using Managers;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;
    
    /// <summary>
    /// If the player walks on the trigger, the level completes.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            gameManager.CompleteLevel();
    }
}
