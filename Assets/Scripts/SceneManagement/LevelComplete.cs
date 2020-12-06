using Entity.Player;
using Managers;
using UnityEngine;

namespace SceneManagement
{
    public class LevelComplete : MonoBehaviour
    {
        /// <summary>
        /// If the player walks on the trigger, the level completes.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerState>())
                GameManager.Instance.ChangeLevel();
        }
    }
}
