using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private bool _gameHasEnded = false;

        public void CompleteLevel()
        {
            Debug.Log("Level won!");
            SceneManager.LoadScene("Scenes/LevelComplete");
        }
    
        public void EndGame()
        {
            if (_gameHasEnded == false)
            {
                _gameHasEnded = true;
                Debug.Log("Game Over!");
            }
        }

        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
