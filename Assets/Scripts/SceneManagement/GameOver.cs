using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private Button backToMainMenu;

        private void Awake()
        {
            backToMainMenu.onClick.AddListener(BackToMainMenu);
        }


        private void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
