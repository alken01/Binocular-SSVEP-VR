using UnityEngine;

public class SSVEP : MonoBehaviour
{
    public float frequency; // Set the frequency in Hertz (cycles per second)
    
    private float dutyCycle;
    private Renderer objectRenderer;
    private Color originalColor;
    private float elapsedTime;
    private bool isOn = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        elapsedTime = 0f;
        dutyCycle = 0.5f;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two)) {
            isOn = !isOn;
        }

        if(isOn){
            elapsedTime += Time.deltaTime;
            
            float t = elapsedTime * frequency;
            float square = Mathf.Repeat(t, 1f) < dutyCycle ? 1f : 0f;

            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, square);
            objectRenderer.material.color = newColor;

        }
    }
}
