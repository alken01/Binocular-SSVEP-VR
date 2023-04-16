using UnityEngine;
using System.Collections.Generic;

public class ExperimentData : MonoBehaviour
{
    private int experimentNumber;
    private ExperimentVisibility experimentVisibility;
    private SSVEPManager ssvepManager;
    private List<GameObject> crossList = new List<GameObject>();

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
        ssvepManager.OnExperimentEnded += HandleExperimentEnded;

        SetVisibility(false);
    }
    public void StartExperiment()
    {
        SetVisibility(true);
        ssvepManager.StartSSVEPManager();
    }


    private void OnDestroy()
    {
        // Unsubscribe from the event
        ssvepManager.OnExperimentEnded -= HandleExperimentEnded;
    }

    public int GetExperimentNumber()
    {
        return experimentNumber;
    }

    public void SetVisibility(bool visible)
    {
        experimentVisibility.SetVisibility(visible);
    }

    private void HandleExperimentEnded()
    {
        // after the experiment has ended
        // set the visibility of the experiment to false
        SetVisibility(false);
        // unsubscribe from the event
        OnDestroy();
        // start the next one
        FindObjectOfType<ExperimentManager>().NextExperiment();
    }

}
