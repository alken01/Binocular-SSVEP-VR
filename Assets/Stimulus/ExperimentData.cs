using UnityEngine;

public class ExperimentData : MonoBehaviour, IExperiment
{
    [SerializeField] private int experimentNumber;
    private ExperimentVisibility experimentVisibility;
    private SSVEPManager ssvepManager;
    private GameObject[] crossObjects;

    private void Awake()
    {
        GlobalVariables.setExperimentNr(experimentNumber);
        
        experimentVisibility = GetComponent<ExperimentVisibility>();
        ssvepManager = GetComponent<SSVEPManager>();

        // Subscribe to the event
        ssvepManager.OnExperimentEnded += HandleExperimentEnded;

        // Set the visibility of the experiment to false
        // experimentVisibility.SetVisibility(false);
    }
    
    public void StartExperiment()
    {
        experimentVisibility.SetVisibility(true);
        ssvepManager.StartSSVEPManager(experimentNumber);
    }

    public void EndExperiment()
    {
        experimentVisibility.SetVisibility(false);
        ssvepManager.OnExperimentEnded -= HandleExperimentEnded;
        FindObjectOfType<ExperimentManager>().NextExperiment();
    }

    public int GetExperimentNumber()
    {
        return experimentNumber;
    }

    public void SetVisibility(bool visible)
    {
        experimentVisibility.SetVisibility(visible);
    }

    public void SetCrossVisibility(bool visible)
    {
        foreach (GameObject crossObject in crossObjects)
        {
            crossObject.SetActive(visible);
        }
    }

    private void HandleExperimentEnded()
    {
        EndExperiment();
    }

}
