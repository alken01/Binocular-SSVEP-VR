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

        client.SendTCP("ExperimentManager started.");
        client.SendTCP("Number of experiments: " + experimentDatas.Length);
    }

   
    private void Update() {
        // If two is pressed, go to the next experiment
        if (OVRInput.GetDown(OVRInput.Button.Two)) {
            client.SendTCP("NextExperiment called. Experiment number: " + currentExperimentNumber);
            NextExperiment();
        }
    }

    public void SetExperiment(int experimentNumber)
    {
        currentExperimentNumber = experimentNumber;
        client.SendTCP("SetExperiment called. Experiment number: " + currentExperimentNumber);
        UpdateExperimentVisibility();
    }

    public void NextExperiment()
    {
        currentExperimentNumber++;
        client.SendTCP("NextExperiment called. Experiment number: " + currentExperimentNumber);
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
            bool shouldBeVisible = experimentData.GetExperimentNumber() == currentExperimentNumber;
            experimentData.SetVisibility(shouldBeVisible);
            client.SendTCP("Experiment " + experimentData.GetExperimentNumber() + " visibility: " + shouldBeVisible);
        }
    }

}
