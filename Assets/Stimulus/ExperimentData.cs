using UnityEngine;

public class ExperimentData : MonoBehaviour
{

    private int experimentNumber;
    private SSVEPManager ssvepManager;
    private int numberOfChildren;
    private int currentTarget;

    private void Start()
    {
        experimentNumber = GlobalVariables.getExperimentNr();
        ssvepManager = GetComponent<SSVEPManager>();
        SetVisibility(false);

        numberOfTargets = transform.childCount;
        currentTarget = -1;
    }


    private void SSVEPChange(bool status)
    {
        // if SSVEP is on hide all the targets' cross
        // else show the current target's cross
        // do a circular increment of the currentTarget
        if (status)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<TargetData>().HideCross();
            }
        }
        else
        {
            currentTarget = (currentTarget + 1) % numberOfTargets;
            transform.GetChild(currentTarget).GetComponent<TargetData>().ShowCross();
        }
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
