using UnityEngine;

namespace Interfaces
{
    public class Singleton<T> : MonoBehaviour where T : class
    {
        /// <summary>
        /// Globally accessible Instance
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// checks if there is already a Singleton of this type.
        /// </summary>
        protected virtual void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this as T;
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
