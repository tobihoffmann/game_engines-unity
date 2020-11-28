using UnityEngine;

namespace LevelGeneration
{
    public class Road : MonoBehaviour
    {
        [SerializeField]
        internal bool north, south, east, west;

        [SerializeField]
        internal bool isJunction;

        [SerializeField]
        internal bool isCurve;

        internal int X { get; set; }
        internal int Y { get; set; }
    }
}
