using UnityEngine;

public class SSVEPBlock : MonoBehaviour{
    // set the frequency of the square wave
    public float frequency; 
    public bool sine = false;
    
    private Renderer objectRenderer;
    private float elapsedTime; 
    private bool isOn = false; 

    void Start(){
        objectRenderer = GetComponent<Renderer>(); 
        elapsedTime = 0f; 
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two)) { 
            isOn = !isOn; 
            Color color = objectRenderer.material.color;
            color.a = 1f;
            objectRenderer.material.color = color;
        }

        if(isOn){
            // add the time since the last frame to the elapsed time
            elapsedTime += Time.deltaTime; 
            float t = elapsedTime * frequency; 
            float value;
            if (sine) {
                value = Mathf.Sin(t * 2 * Mathf.PI); 
            }
            // else it is a square wave
            else{
                if (t < 0.5) {
                    value = 1f;
                } else {
                    value = 0f;
                }
            }

            // change the alpha value
            Color color = objectRenderer.material.color;
            color.a = value;
            objectRenderer.material.color = color;
            // reset the elapsed time
            if (value == 0f) {
                elapsedTime = 0f;
            }
        }
    }
}
