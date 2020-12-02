using UnityEngine;

namespace LevelGeneration
{
    public class Border : MonoBehaviour
    {
        [Header("Openings")]
        [SerializeField]
        internal bool north;
        [SerializeField]
        internal bool south;
        [SerializeField]
        internal bool east;
        [SerializeField]
        internal bool west;
        
        [Header("Attributes")]
        [SerializeField]
        internal bool isCorner;

        internal int X { get; set; }
        internal int Y { get; set; }
    }
}
