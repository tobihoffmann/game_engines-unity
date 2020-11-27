using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class TileTemplates : MonoBehaviour
    {
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
    }
}