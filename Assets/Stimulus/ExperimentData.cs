using UnityEngine;

public class ExperimentData : MonoBehaviour
{
    private int experimentNumber;
    
    public int GetExperimentNumber()
    {
        return experimentNumber;
    }

    public void SetExperimentNumber(int number)
    {
        experimentNumber = number;
    }

    public void SetVisibility(bool visible)
    {
        // adjust the SSVEP Manager
        SSVEPManager ssvepManager = GetComponent<SSVEPManager>();
        ssvepManager.SetStart(visible);
        // make it visible
        gameObject.SetActive(visible);
    }

}
