using UnityEngine;

public class ExperimentData : MonoBehaviour
{
    private int experimentNumber;
    private SSVEPManager ssvepManager;

    private void Start()
    {
        experimentNumber = GlobalVariables.getNextNr();
        ssvepManager = GetComponent<SSVEPManager>();
        SetVisibility(false);
    }

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
        ssvepManager.SetStart(visible);
        // make it visible
        gameObject.SetActive(visible);
    }

}
