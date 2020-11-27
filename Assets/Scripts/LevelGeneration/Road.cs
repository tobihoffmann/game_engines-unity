using UnityEngine;

namespace LevelGeneration
{
    public class Road : MonoBehaviour
    {
        [SerializeField]
        internal bool north, south, east, west;

        internal int X { get; set; }
        internal int Y { get; set; }
        
        internal bool IsJunction()
        {
            int exits = 0;
            
            if (north) exits++;
            if (east) exits++;
            if (south) exits++;
            if (west) exits++;

            if (exits >= 3)
            {
                return true;
            }
            return false;
        }
        
        internal bool IsXJunction()
        {
            int exits = 0;
            
            if (north) exits++;
            if (east) exits++;
            if (south) exits++;
            if (west) exits++;

            if (exits >= 4)
            {
                return true;
            }
            return false;
        }
        
        internal bool IsTJunction()
        {
            int exits = 0;
            
            if (north) exits++;
            if (east) exits++;
            if (south) exits++;
            if (west) exits++;

            if (exits == 3)
            {
                return true;
            }
            return false;
        }
    }
}
