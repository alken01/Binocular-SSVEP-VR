using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    // public Vector3[] positions;
    // private int index = 0;
    // private bool visible = true;
    // private SSVEPManager pauseSSVEP;
    // private Renderer objectRenderer;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     // get the PauseSSVEP script from the parent
    //     pauseSSVEP = GetComponentInParent<SSVEPManager>();
    //     objectRenderer = GetComponent<Renderer>();
    //     pauseSSVEP.changeMaxArrows(positions.Length);
    // }

    // private void Update()
    // {
    //     // check if SSVEP is paused
    //     SetVisibility();
    //     GetIndex();
    //     // set the object's position to the position at the current index
    //     if(visible) transform.position = positions[index];
    //     // else make the object invisible
    //     else objectRenderer.material.color = new Color(1f, 1f, 1f, 0f);

    // }

    // public void GetIndex()
    // {
    //     index = pauseSSVEP.getCurrentTarget();
    // }

    // // set the visibility of the object if the PauseSSVEP is paused
    // public void SetVisibility()
    // {
    //     visible = !pauseSSVEP.GetPauseStatus();
    // }
    

}
