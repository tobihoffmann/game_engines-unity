using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private bool _gameHasEnded = false;

        [SerializeField]
        private GameObject levelChanger;

        public void ChangeLevel()
        {
            levelChanger.GetComponent<LevelChanger>();
            int currentLevel = GetCurrentLevelIndex();
            
            if (currentLevel >= GetSceneCount())
                currentLevel = -1;
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(currentLevel + 1);
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

        public int GetCurrentLevelIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public int GetSceneCount()
        {
            return SceneManager.sceneCount;
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
