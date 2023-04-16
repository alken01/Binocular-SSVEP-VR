using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    private int currentExperimentNumber;
    private TCPClient client;
    private ExperimentData[] experimentDatas;
    // private bool experimentsStarted = false;

    private void Awake()
    {
        OVRManager.display.displayFrequency = 90f;

        client = GetComponent<TCPClient>();
        
        // get all the ExperimentData objects
        experimentDatas = FindObjectsOfType<ExperimentData>();

        client.SendTCP("ExperimentManager started.");
        client.SendTCP("Number of experiments: " + experimentDatas.Length);

        // Set all the experiments to invisible
        foreach (ExperimentData experimentData in experimentDatas)
        {
            experimentData.SetVisibility(false);
        }
        
        currentExperimentNumber = -1;
        // experimentsStarted = false;
    }


    public void StartExperimentManager(){
        currentExperimentNumber = 0;
        // experimentsStarted = true;
        UpdateExperimentVisibility();
    }


    public void NextExperiment()
    {
        currentExperimentNumber++;
        if (currentExperimentNumber == experimentDatas.Length)
        {
            // Stop the experiments
            // experimentsStarted = false;
            client.SendTCP("NextExperiment called. All experiments are done.");
            return;
        }
        else
        {
            client.SendTCP("NextExperiment called. Experiment number: " + currentExperimentNumber);
            UpdateExperimentVisibility();
        }
    }


    private void UpdateExperimentVisibility()
    {
        foreach (ExperimentData experimentData in experimentDatas)
        {
            bool shouldBeVisible = experimentData.GetExperimentNumber() == currentExperimentNumber;
            client.SendTCP("Experiment " + experimentData.GetExperimentNumber() + " visibility: " + shouldBeVisible + " (current experiment: " + currentExperimentNumber + ")");
            if (shouldBeVisible){
                experimentData.StartExperiment();
            }
            else{
                experimentData.SetVisibility(false);
            }
        }
    }
}
