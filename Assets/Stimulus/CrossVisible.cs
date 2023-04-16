using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossVisible : MonoBehaviour
{
    private GameObject cross;

    void Start()
    {
        // get the Cross Object in the children
        cross = transform.Find("Cross").gameObject;
        // set the visibility of the cross to false
        cross.SetActive(false);
    }

    public void SetCrossVisibility(bool visible)
    {
        cross.SetActive(visible);
    }

    
}
