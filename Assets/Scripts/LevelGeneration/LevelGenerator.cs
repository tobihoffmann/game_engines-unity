using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    /*
    private void ManageJunction()
    {
        if (newTile.isJunction)
        {
            if (newTile.isJunctionThree)
            {
                _junctionsThree++;
                if (_junctionsThree >= _maxJunctionsThree)
                    removealljunctionsthree();
            } else if (newTile.isJunctionFour)
            {
                _junctionsFour++;
                if (_junctionsFour >= _maxJunctionsFour)
                    removealljunctionsfour();
            }
        }
    }
    */
    
    

    private bool canPlaceJunction()
    {
        // if junctionsOf4 >= maxJunctionsOf4 || junctionsOf3 >= maxJunctionsOf3 || is previous tile a junction
        // return false
        // else return true
        return false;
    }
}