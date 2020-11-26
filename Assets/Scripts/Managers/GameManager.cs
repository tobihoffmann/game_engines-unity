using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
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
        
        public static void Quit()
        {
#if UNITY_EDITOR //Exits the play-mode
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

    }
}
