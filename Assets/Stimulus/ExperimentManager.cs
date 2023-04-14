using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    private int currentExperimentNumber;
    private TCPClient client;
    private ExperimentData[] experimentDatas;

    private void Awake()
    {
        // set ovr refresh rate to 90Hz
        OVRManager.display.displayFrequency = 90f;
        
        // initialize the TCP client
        client = GetComponent<TCPClient>();
        experimentDatas = GetComponentsInChildren<ExperimentData>();

        // set the current experiment number to -1
        SetExperiment(-1);
    }

   
    private void FixedUpdate() {
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
