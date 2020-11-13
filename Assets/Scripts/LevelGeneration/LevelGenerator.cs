using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private void ManageJunction()
    {
        // this is property
        int junctionsOfThree;
        int junctionsOfFour;
        // 1. check if new tile is junction
            // if junctionsOfThree >= maxJunctionsOfThree || if junctionsOfFour >= maxJunctionsOfFour
                // if junctionsOfThree >= maxJunctionsOfThree
                    // then delete all junctions of 3 from arrays
                // else if junctionsOfFour >= maxJunctionsOfFour
                    // then delete all junctions of 4 from arrays
            // else
                // check which junction (3,4)?
                    // if 3 then junctionsOfThree++
                        // if junctionsOfThree >= maxJunctionsOfThree
                        // then delete all junctions of 3 from arrays
                    // else 4 then junctionsOfFour++
    }

    private bool canPlaceJunction()
    {
        // if junctionsOf4 >= maxJunctionsOf4 || junctionsOf3 >= maxJunctionsOf3 || is previous tile a junction
            // return false
        // else return true
        return false;
    }
}
