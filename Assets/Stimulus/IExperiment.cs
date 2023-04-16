using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExperiment 
{
    void StartExperiment();
    void EndExperiment();
    int GetExperimentNumber();
}
