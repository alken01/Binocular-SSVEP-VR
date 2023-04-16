using UnityEngine;
using System;
using System.Collections;


public class SSVEPManager : MonoBehaviour {
    [SerializeField] private string startMsg = "start";
    [SerializeField] private string resumeMsg = "resume";
    [SerializeField] private string pauseMsg = "pause";
    [SerializeField] private float epochTime = 6f;
    [SerializeField] private float pauseTime = 3f;
    [SerializeField] private int numberOfCycles = 3;

    private TCPClient client;
    private SSVEP[] ssvepComponents;
    private ExperimentData experimentData;
    private bool start = false;

    // public event Action OnExperimentEnded;

    private void Start() {
        client = GetComponentInParent<TCPClient>();
        ssvepComponents = GetComponentsInChildren<SSVEP>(true);
        experimentData = GetComponent<ExperimentData>();

        startMsg = "start " + experimentData.GetExperimentNumber();
        resumeMsg = "resume " + experimentData.GetExperimentNumber();
        pauseMsg = "pause " + experimentData.GetExperimentNumber();

        SetSSVEPComponents(false);
    }

    public void StartSSVEPManager() {
        start = true;
        StartCoroutine(RunExperiment());
    }

    public void StopSSVEPManager()
    {
        if(!start) return;
        StopAllCoroutines();
        SetSSVEPComponents(false);
        client.SendTCP("Experiment " + experimentData.GetExperimentNumber() + " ended.");
    }

    private IEnumerator RunExperiment() {
        client.SendTCP(startMsg);

        for(int i = 0; i < numberOfCycles; i++) {
            // Activate SSVEP components
            SetSSVEPComponents(true);

            // Send resume message to TCP client
            client.SendTCP(resumeMsg+ " " + i);

            // Wait epoch time
            yield return new WaitForSeconds(epochTime);

            // Deactivate SSVEP components
            SetSSVEPComponents(false);

            // Send pause message to TCP client
            client.SendTCP(pauseMsg+ " " + i);

            // Wait pause time
            yield return new WaitForSeconds(pauseTime);
        }
        StopSSVEPManager();
    }


    private void SetSSVEPComponents(bool status) {
        foreach (SSVEP ssvep in ssvepComponents) {
            // make sure the target is fully visible
            if (!status) ssvep.StopSSVEP();
            // disable the SSVEP script
            ssvep.enabled = status;
        }
    }
}
