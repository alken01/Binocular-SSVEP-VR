using UnityEngine;
using System;
using System.Collections;

public class SSVEPManager : MonoBehaviour 
{
    [SerializeField] private string startMsg = "start";
    [SerializeField] private string resumeMsg = "resume";
    [SerializeField] private string pauseMsg = "pause";
    [SerializeField] private float epochTime = 2f;
    [SerializeField] private float pauseTime = 1f;
    [SerializeField] private int numberOfCycles = 2;

    private TCPClient client;
    private SSVEP[] ssvepComponents;
    private bool start = false;
    public event Action OnExperimentEnded;
    private IExperiment experiment;
    private int experimentNumber;

    private int currentTarget = 0;
    private int numberOfTargets = 0;

    private CrossVisible[] crossObjects;

    private void Start()
    {
        client = GetComponentInParent<TCPClient>();
        ssvepComponents = GetComponentsInChildren<SSVEP>(true);
        experiment = GetComponent<IExperiment>() as IExperiment;   
        
        // get the CrossVisible scripts in children
        crossObjects = GetComponentsInChildren<CrossVisible>(true);
        numberOfTargets = crossObjects.Length;
        
    }

    public void StartSSVEPManager(int number) 
    {
        start = true;
        experimentNumber = number;

        startMsg = "start_" + experimentNumber;
        resumeMsg = "resume_" + experimentNumber;
        pauseMsg = "pause_" + experimentNumber;
        StartCoroutine(RunExperiment());
    }

    public void StopSSVEPManager()
    {
        if(!start) return;
        StopAllCoroutines();
        SetSSVEPComponents(false);
        client.SendTCP($"Experiment {experimentNumber} finished.");

        // tell the experiment manager that the experiment has ended, so it can start the next one
        OnExperimentEnded?.Invoke();
    }

    private IEnumerator RunExperiment() 
    {        
        int currentTarget = -1;
        for(int i = 0; i < numberOfCycles*numberOfTargets; i++) {

            // Deactivate SSVEP components
            SetSSVEPComponents(false);

            // Send pause message to TCP client
            if(currentTarget >= 0){
                // this pause corresponds to the previous target        
                client.SendTCP(pauseMsg + currentTarget);
            }

            // Show next target's cross
            ShowCross();

            // Wait pause time
            yield return new WaitForSeconds(pauseTime);

            // Hide cross
            HideCross();

            // Activate SSVEP components
            SetSSVEPComponents(true);

            // update the current target
            currentTarget = i % numberOfTargets;

            // Send resume message to TCP client
            client.SendTCP(resumeMsg + currentTarget);

            // Wait epoch time
            yield return new WaitForSeconds(epochTime);
        }
        // send the last pause message to TCP client
        client.SendTCP(pauseMsg + currentTarget);

        StopSSVEPManager();
    }


    private void SetSSVEPComponents(bool status) 
    {
        foreach (SSVEP ssvep in ssvepComponents) 
        {
            // make sure the target is fully visible
            if (!status) ssvep.StopSSVEP();
            // disable the SSVEP script
            ssvep.enabled = status;
        }
    }


    public void ShowCross() 
    {
        crossObjects[currentTarget].SetCrossVisibility(true);
    }

    public void HideCross() 
    {
        crossObjects[currentTarget].SetCrossVisibility(false);
        currentTarget = (currentTarget + 1) % crossObjects.Length;
    }



}


