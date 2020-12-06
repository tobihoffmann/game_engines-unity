using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class LevelChanger : MonoBehaviour
    {
        private Animator _animator;

        private int _levelToLoad;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    
        public void FadeToLevel(int levelIndex)
        {
            _levelToLoad = levelIndex;
            _animator.SetTrigger("FadeOut");
        }

        public void OnFadeComplete()
        {
            SceneManager.LoadScene(_levelToLoad);
        }
    }
}
