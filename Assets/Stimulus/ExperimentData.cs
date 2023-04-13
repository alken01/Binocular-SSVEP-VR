using UnityEngine;

public class ExperimentData : MonoBehaviour
{
    private int experimentNumber;
    private int maxTargets;
    private int currentTarget;
    private SSVEPManager ssvepManager;

    private void Start()
    {
        maxTargets = transform.childCount;
        currentTarget = 0;
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
