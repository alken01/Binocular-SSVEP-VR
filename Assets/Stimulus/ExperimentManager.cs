using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    private int currentExperimentNumber;
    private TCPClient client;
    private ExperimentData[] experimentDatas;

    private void Awake()
    {
        currentExperimentNumber = -1;
        client = GetComponent<TCPClient>();
        experimentDatas = GetComponentsInChildren<ExperimentData>();

        for (int i = 0; i < experimentDatas.Length; i++)
        {
            experimentDatas[i].SetExperimentNumber(i); // give all Experiments a unique experimentNumber
            experimentDatas[i].SetVisibility(false); // make it invisible at the start
        }
        // nothing at start
        SetExperiment(currentExperimentNumber);
    }

   
    private void Update() {
        // If two is pressed, go to the next experiment
        if (OVRInput.GetDown(OVRInput.Button.Two)) {
            NextExperiment();
        }
    }

    public void SetExperiment(int experimentNumber)
    {
        currentExperimentNumber = experimentNumber;
        UpdateExperimentVisibility();
    }

    public void NextExperiment()
    {
        currentExperimentNumber++;
        // if the currentExperimentNumber is out of bounds, set it to -1
        if (currentExperimentNumber >= experimentDatas.Length)
        {
            currentExperimentNumber = -1;
        }
        UpdateExperimentVisibility();
    }

    private void UpdateExperimentVisibility()
    {
        foreach (ExperimentData experimentData in experimentDatas)
        {
            // adjust to make visible only the currentExperimentNumber 
            experimentData.SetVisibility(experimentData.GetExperimentNumber() == currentExperimentNumber);
        }
    }
}
