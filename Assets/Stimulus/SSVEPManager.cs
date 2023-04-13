using UnityEngine;

public class SSVEPManager : MonoBehaviour {
    [SerializeField] private string startMsg = "start";
    [SerializeField] private string resumeMsg = "resume";
    [SerializeField] private string pauseMsg = "pause";
    [SerializeField] private float epochTime = 6f;
    [SerializeField] private float pauseTime = 3f;

    private bool SSVEP;
    private bool isPaused;
    private float elapsedTime;
    
    private TCPClient client;
    
    private ExperimentData experimentData;
    private SSVEP[] ssvepComponents;
    private bool start;

    private void Awake() {
        experimentData = GetComponent<ExperimentData>();
        startMsg = "Experiment " + experimentData.GetExperimentNumber() + ": " + startMsg;
        resumeMsg = "Experiment " + experimentData.GetExperimentNumber() + ": " + resumeMsg;
        pauseMsg = "Experiment " + experimentData.GetExperimentNumber() + ": " + pauseMsg;
        client = GetComponentInParent<TCPClient>();
        ssvepComponents = GetComponentsInChildren<SSVEP>(true);
    }

    public void SetStart(bool set){
        start = set;
        SSVEP = set;
        AdjustSSVEP(start);
        if(set) client.SendTCP(startMsg);
    }

    private void Update() {
        if (start != true) return;

        elapsedTime += Time.deltaTime;

        if (SSVEP) {
            // if epoch time is over
            if (elapsedTime >= epochTime) {
                // disable the SSVEP 
                SSVEP = false;
                AdjustSSVEP(SSVEP);
                // send the pause message
                client.SendTCP(pauseMsg);
                // reset the timer
                elapsedTime = 0f;
            }
        } else { // no SSVEP atm
            // if pause time is over
            if (elapsedTime >= pauseTime) {
                // enable the SSVEP 
                SSVEP = true;
                AdjustSSVEP(SSVEP);
                // send the resume message
                client.SendTCP(resumeMsg);
                // reset the timer
                elapsedTime = 0f;
            }
        }
    }

    private void AdjustSSVEP(bool status) {
    // Loop through all the ssvep components
        foreach (SSVEP ssvep in ssvepComponents) {
            // if false, return the alpha value to 1f
            if (!status) ssvep.StopSSVEP();
            // then disable the script
            ssvep.enabled = status;
        }
    }

}
