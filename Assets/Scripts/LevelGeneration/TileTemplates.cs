using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class TileTemplates : MonoBehaviour
    {
        [Header("Roads")]
        [SerializeField]
        internal List<GameObject> roadTiles;
        
        [SerializeField]
        internal GameObject endingNorth;
        
        [SerializeField]
        internal GameObject endingEast;
        
        [SerializeField]
        internal GameObject endingSouth;
        
        [SerializeField]
        internal GameObject endingWest;

        [Header("Planes")]
        [SerializeField]
        internal List<GameObject> planeTiles;
        
        [Header("Level Borders")]
        [SerializeField]
        internal List<GameObject> borders;

        [SerializeField]
        internal GameObject borderCornerNorthEast;
        [SerializeField]
        internal GameObject borderCornerNorthWest;
        [SerializeField]
        internal GameObject borderCornerSouthEast;
        [SerializeField]
        internal GameObject borderCornerSouthWest;
    }
}