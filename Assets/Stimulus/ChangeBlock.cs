using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBlock : MonoBehaviour
{
    // Script to show or hide a block when a button is pressed on the controller
    // depending on the current state of the block
    public int targetBlockNumber;
    public int maxBlockNumber;

    private int currentBlockNumber;
    private Renderer[] renderers;

    void Start(){
        currentBlockNumber = 0;
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        { 
            currentBlockNumber++;

            if (currentBlockNumber >= maxBlockNumber)
            {
                currentBlockNumber = 0;
            }
            
            if (currentBlockNumber == targetBlockNumber)
            {
                SetBlockVisibility(true);
            }
            else
            {
                SetBlockVisibility(false);
            }
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
