using UnityEngine;

public class SSVEPBlock : MonoBehaviour{
    // set the frequency of the square wave
    public float frequency; 
    public bool sine;
    
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
            // if (!isOn) {
            //     elapsedTime = 0f; // reset the elapsed time when turning off the wave
            // }
        }

        if(isOn){
            // add the time since the last frame to the elapsed time
            elapsedTime += Time.deltaTime; 
            float t = elapsedTime * frequency;
            float value;

            if(sine){ // it is a sine wave
                value = (Mathf.Sin(t * Mathf.PI * 2f) + 1f) / 2f;
            } else { // it is a square wave
                value = Mathf.Repeat(t, 1f) < 0.5f ? 0f : 1f;
            }

            // // reset elapsed time if it is greater than 1
            // if (elapsedTime >= 1f / frequency) {
            //     elapsedTime = 0f;
            // }
            
            // change the alpha value
            Color color = objectRenderer.material.color;
            color.a = value;
            objectRenderer.material.color = color;
        }
    }
}
