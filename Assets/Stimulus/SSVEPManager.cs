using UnityEngine;
using System.Collections;

public class SSVEPManager : MonoBehaviour {
    [SerializeField] private string startMsg = "start";
    [SerializeField] private string resumeMsg = "resume";
    [SerializeField] private string pauseMsg = "pause";
    [SerializeField] private float epochTime = 6f;
    [SerializeField] private float pauseTime = 3f;

    private TCPClient client;
    private SSVEP[] ssvepComponents;
    private ExperimentData experimentData;

    private void Awake() {
        client = GetComponentInParent<TCPClient>();
        ssvepComponents = GetComponentsInChildren<SSVEP>(true);
        experimentData = GetComponent<ExperimentData>();
    }

    public void StartSSVEP() {
        StartCoroutine(RunExperiment());
    }

    public void StopSSVEP() {
        StopAllCoroutines();
        SetSSVEPComponents(false);
    }

    private IEnumerator RunExperiment() {
        client.SendTCP(startMsg);
        while (true) {
            // Activate SSVEP components
            SetSSVEPComponents(true);

            // Send resume message to TCP client
            client.SendTCP(resumeMsg);

            // Wait epoch time
            yield return new WaitForSeconds(epochTime);

            // Deactivate SSVEP components
            SetSSVEPComponents(false);

            // Send pause message to TCP client
            client.SendTCP(pauseMsg);

            // Wait pause time
            yield return new WaitForSeconds(pauseTime);
        }
    }

    private void SetSSVEPComponents(bool status) {
        foreach (SSVEP ssvep in ssvepComponents) {
            if (!status) ssvep.StopSSVEP();
            ssvep.enabled = status;
            // experimentData.SSVEPChange(status);
        }
    }
}
