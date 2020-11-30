using UnityEngine;

namespace LevelGeneration
{
    public class Road : MonoBehaviour
    {
        [Header("Openings")]
        [SerializeField]
        internal bool north, south, east, west;

        [Header("Attributes")]
        [SerializeField]
        internal bool isJunction;

        [SerializeField]
        internal bool isCurve;

        internal int X { get; set; }
        internal int Y { get; set; }
    }
}
