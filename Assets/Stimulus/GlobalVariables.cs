using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static class for GlobalVariables
public static class GlobalVariables
{
    public static int EXPERIMENT_NR = 0;
    public static List<List<int>> TARGETS = new List<List<int>>();

    public static int getExperimentNr()
    {
        TARGETS.Add(new List<int>());
        EXPERIMENT_NR++;
        return EXPERIMENT_NR - 1;
    }

    public static int getTargetNr(int experimentNumber)
    {
        if (experimentNumber < 0 || experimentNumber >= EXPERIMENT_NR)
        {
            // Throw an exception if the experiment number is invalid
            throw new System.ArgumentException("Invalid experiment number.");
        }

        TARGETS[experimentNumber].Add(TARGETS[experimentNumber].Count);
        return TARGETS[experimentNumber].Count - 1;
    }
}