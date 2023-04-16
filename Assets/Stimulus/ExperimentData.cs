using UnityEngine;
using System.Collections.Generic;

public class ExperimentData : MonoBehaviour
{
    private int experimentNumber;
    private ExperimentVisibility experimentVisibility;
    private SSVEPManager ssvepManager;
    private List<GameObject> crossList = new List<GameObject>();

    private bool experimentEnded = false;

    private void Awake()
    {
        experimentNumber = GlobalVariables.getExperimentNr();
        experimentVisibility = GetComponent<ExperimentVisibility>();
        ssvepManager = GetComponent<SSVEPManager>();

        // Get all the crosses, they are grandchildren of the ExperimentData object
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.gameObject.name == "Cross")
                {
                    crossList.Add(grandchild.gameObject);
                }
            }
        }

        // Subscribe to the event
        // ssvepManager.OnExperimentEnded += HandleExperimentEnded;
    }
    public void StartExperiment()
    {
        if(!experimentEnded)
            SetVisibility(true);
    }


    private void OnDestroy()
    {
        // Unsubscribe from the event
        // ssvepManager.OnExperimentEnded -= HandleExperimentEnded;
    }

    public int GetExperimentNumber()
    {
        return experimentNumber;
    }

    public void SetVisibility(bool visible)
    {
        experimentVisibility.SetVisibility(visible);
    }

    // private void HandleExperimentEnded()
    // {
    //     FindObjectOfType<ExperimentManager>().NextExperiment();
    //     experimentEnded = true;
    // }

    // public void SetCrossVisibility(int target, bool visible)
    // {
    //     if (target >= 0 && target < crossList.Count)
    //     {
    //         crossList[target].SetActive(visible);
    //     }
    // }

}
