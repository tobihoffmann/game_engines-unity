using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameAudio
{
    public class Soundtrack : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlaySoundtrack();
        }
        
        private void PlaySoundtrack()
        {
            int activeScene = SceneManager.GetActiveScene().buildIndex;
            
            
            switch (activeScene)
            {
                case 0:
                    AudioManager.Instance.FadeIn("SoundTrackDrone",4);
                    break;
                
                case 1:
                    AudioManager.Instance.FadeIn("SoundTrackWasteland", 1);
                    break;
            }
        }
    }
}