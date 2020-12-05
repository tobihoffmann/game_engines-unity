using Entity.Player;
using Interfaces;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private LevelChanger _lc;
        protected override void Awake()
        {
            _lc = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
            base.Awake();
            PlayerState.OnPlayerDeath += EndGame;
        }

        public void ChangeLevel()
        {
            int currentLevel = GetCurrentLevelIndex();
            
            if (currentLevel >= GetSceneCount())
                currentLevel = -1;
            _lc.FadeToLevel(currentLevel + 1);
        }


        /// <summary>
        /// When the player dies, load last game scene in build index. Game Over should always be last index in build settings!
        /// </summary>
        private void EndGame()
        {
            _lc.FadeToLevel(SceneManager.sceneCountInBuildSettings - 1);
            PlayerState.OnPlayerDeath -= EndGame;
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
