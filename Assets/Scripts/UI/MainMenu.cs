using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject continueButton;
    
        [SerializeField]
        private Button newGameButton;
    
        [SerializeField]
        private Button quitButton;
    
    
        private void Awake()
        {
            CheckActiveGameSession();
            continueButton.GetComponent<Button>().onClick.AddListener(Continue);
            newGameButton.onClick.AddListener(StartNewGame);
            quitButton.onClick.AddListener(GameManager.Quit);
        }



        private void StartNewGame()
        {
            //TODO: Destroys PlayerManager Singleton then loads GameScene
            GameManager.Instance.ChangeLevel();
        }

        private void Continue()
        {
            Debug.Log("Continue Game");
        }

        private void CheckActiveGameSession()
        {
            continueButton.SetActive(PlayerManager.Instance != null);
        }
    }
}
