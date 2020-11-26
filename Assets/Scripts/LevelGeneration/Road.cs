using UnityEngine;

namespace LevelGeneration
{
    public class Road : MonoBehaviour
    {
        [SerializeField]
        internal bool north, south, east, west;

        internal int X { get; set; }
        internal int Y { get; set; }
    }
}
