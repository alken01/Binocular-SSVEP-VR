using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    private int currentExperimentNumber;
    private TCPClient client;

    public void Start(){
        currentExperimentNumber = -1;
        client = GetComponent<TCPClient>();
        // give all children who have the ExperimentData script a unique experimentNumber
        int number = 0;
        foreach (Transform child in transform)
        {
            ExperimentData experimentData = child.GetComponent<ExperimentData>();
            if (experimentData != null)
            {
                experimentData.SetExperimentNumber(number);
                number++;
            }
            // also make them invisible
            experimentData.SetVisibility(false);
        }
    }

    public void SetExperiment(int experimentNumber)
    {
        currentExperimentNumber = experimentNumber;
        UpdateExperimentVisibility();
    }

    private void UpdateExperimentVisibility()
    {
        // make all the children that Contain ExperimentData inactive
        foreach (Transform child in transform)
        {
            ExperimentData experimentData = child.GetComponent<ExperimentData>();
            if (experimentData != null)
            {
                // if the experimentNumber of the child is equal to the currentExperimentNumber
                // then set the child to active
                if(experimentData.GetExperimentNumber() == currentExperimentNumber)
                    experimentData.SetVisibility(true);
                else
                    experimentData.SetVisibility(false);
            }
        }
    }


}
