using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static class for GlobalVariables
public static class GlobalVariables
{
    // static variable for the player's score
    public static int MAX_NR = 0;
    public static int CURRENT_NR = 0;

    public static int getNextNr(){
        GlobalVariables.MAX_NR++;
        return GlobalVariables.MAX_NR-1;
    }
    

}