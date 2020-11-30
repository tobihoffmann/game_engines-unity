using UnityEngine;


namespace LevelGeneration
{
    public class LevelTile : MonoBehaviour
    {
        internal enum Type {Road, Plane, Border};
        
        [SerializeField]
        internal Type type;
    }
}
