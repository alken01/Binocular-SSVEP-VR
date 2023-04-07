using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBlock : MonoBehaviour
{
    // Script to show or hide a block when a button is pressed on the controller
    // depending on the current state of the block
    private int targetBlockNumber;

    private int currentBlockNumber;
    private Renderer[] renderers;

    void Start(){
        targetBlockNumber = GlobalVariables.getNextNr();
        currentBlockNumber = 0; 
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        { 
            currentBlockNumber++;
        }     

        if (OVRInput.GetDown(OVRInput.Button.Two))
        { 
            currentBlockNumber--;
        }

        if (currentBlockNumber >= GlobalVariables.MAX_NR){
            currentBlockNumber = 0;
        }

        if(currentBlockNumber<0){
            currentBlockNumber = GlobalVariables.MAX_NR-1;
        }
        
        if (currentBlockNumber == targetBlockNumber){
            SetBlockVisibility(true);
        }
        else
        {
            SetBlockVisibility(false);
        }
              
    }

    void SetBlockVisibility(bool set)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = set;
        }
    }
}
