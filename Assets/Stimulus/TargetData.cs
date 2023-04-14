using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetData : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject cross;
    private int targetNumber;
    private ExperimentData experimentData;
    void Start()
    {
        // get the experimentData from the parent
        experimentData = transform.parent.GetComponent<ExperimentData>();
        
        // check if the target has a child called cross
        if (transform.Find("Cross") != null)
        {
            // if it has, save the cross object
            cross = transform.Find("Cross").gameObject;
        }
        // get the target number
        targetNumber = GlobalVariables.getTargetNr(experimentData.getExperimentNumber());

    }

    // HideCross() hides the cross
    public void HideCross()
    {
        if (cross != null)
        {
            cross.SetActive(false);
        }
    }

    // ShowCross() shows the cross
    public void ShowCross()
    {
        if (cross != null)
        {
            cross.SetActive(true);
        }
    }
    
}
