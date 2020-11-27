using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class TileTemplates : MonoBehaviour
    {
        [SerializeField]
        internal List<GameObject> roadTiles;
        
        [SerializeField]
        internal List<GameObject> roadEndings;
    }
}