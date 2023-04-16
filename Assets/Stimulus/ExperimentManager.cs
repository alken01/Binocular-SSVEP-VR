using System.Linq;
using UnityEngine;
using System.Collections;

public class ExperimentManager : MonoBehaviour
{
    private int currentExperimentNumber = -1;
    private TCPClient client;
    private ExperimentData[] experimentDatas;

    private void Awake()
    {
        OVRManager.display.displayFrequency = 90f;

        client = GetComponent<TCPClient>();
        
        // get all the ExperimentData objects
        experimentDatas = FindObjectsOfType<ExperimentData>();

        client.SendTCP("ExperimentManager started.");
        client.SendTCP($"Number of experiments: {experimentDatas.Length}");

    }

    public void StartExperimentManager()
    {
        NextExperiment();
    }

    private bool isWaitingForInput = false;

    public void NextExperiment()
    {
        // turn off the current experiment
        if (currentExperimentNumber >= 0)
        {
            experimentDatas[currentExperimentNumber].SetVisibility(false);
        }
        else{
            // turn all off
            foreach (ExperimentData experimentData in experimentDatas)
            {
                experimentData.SetVisibility(false);
            }
        }

        // increment the experiment number
        currentExperimentNumber++;
        if (currentExperimentNumber >= experimentDatas.Length)
        {
            // Stop the experiments
            client.SendTCP("NextExperiment called. All experiments are done.");
            return;
        }

        // else we show the next experiment
        client.SendTCP($"NextExperiment called. Showing experiment {currentExperimentNumber}.");

        // set the wait state
        isWaitingForInput = true;

        // start the coroutine to wait for user input
        StartCoroutine(WaitForInputAndStartExperiment());
    }

    private IEnumerator WaitForInputAndStartExperiment()
    {
        // wait until the user presses a button
        while (isWaitingForInput)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                isWaitingForInput = false;
            }
            yield return null;
        }

        // start the experiment
        experimentDatas[currentExperimentNumber].StartExperiment();
    }

}
