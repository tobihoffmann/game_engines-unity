
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator _animator;

    private int _levelToLoad;
    private void Start()
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
