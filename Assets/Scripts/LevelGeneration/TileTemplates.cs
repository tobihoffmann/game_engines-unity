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
        internal GameObject borderNorth;
        [SerializeField]
        internal GameObject borderSouth;
        [SerializeField]
        internal GameObject borderEast;
        [SerializeField]
        internal GameObject borderWest;
        [SerializeField]
        internal GameObject borderNorthEast;
        [SerializeField]
        internal GameObject borderNorthWest;
        [SerializeField]
        internal GameObject borderSouthEast;
        [SerializeField]
        internal GameObject borderSouthWest;
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