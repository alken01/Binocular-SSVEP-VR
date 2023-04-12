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
    public int maxTargets;
    private ExperimentData experimentData;
    private bool start;

    private void Start() {
        experimentData = GetComponent<ExperimentData>();
        startMsg = "SSVEP Experiment: " + experimentData.GetExperimentNumber() + " : " + startMsg;
        resumeMsg = "SSVEP Experiment: " + experimentData.GetExperimentNumber() + " : " + resumeMsg;
        pauseMsg = "SSVEP Experiment: " + experimentData.GetExperimentNumber() + " : " + pauseMsg;

        client = GetComponentInParent<TCPClient>();
        elapsedTime = 0f;
        // maxTargets is the number of children
        maxTargets = transform.childCount;
        SSVEP = false;
        start = false;
    }

    private void Update() {
        if (start) {
            ActivateSSVEP();
        }
    }
    public void SetStart(bool set)
    {
        start = set;
        AdjustSSVEP(start);
    }

    private void ActivateSSVEP() {
        // increment the timer
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
        // loop through all the children
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children) {
            // adjust the SSVEP script in the children
            SSVEP ssvep = child.GetComponent<SSVEP>();
            if (ssvep != null) {
                ssvep.enabled = status;
            }

            // check for the grandchildren
            Transform[] grandChildren = child.GetComponentsInChildren<Transform>(true);
            foreach (Transform grandChild in grandChildren) {
                // adjust the SSVEP script in the grandchildren
                SSVEP grandChildSSVEP = grandChild.GetComponent<SSVEP>();
                if (grandChildSSVEP != null) {
                    grandChildSSVEP.enabled = status;
                }
            }
        }
    }
}
