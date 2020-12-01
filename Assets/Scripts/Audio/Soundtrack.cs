using Managers;
using UnityEngine;

namespace Audio
{
    public class Soundtrack : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            AudioManager.Instance.FadeIn("SoundTrackWasteland", 1);
        }
    
    }
}
