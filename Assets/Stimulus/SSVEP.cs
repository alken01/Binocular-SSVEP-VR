using UnityEngine;

public class SSVEP : MonoBehaviour {
    [SerializeField] private float frequency;
    [SerializeField] private bool sine;
    
    private Renderer objectRenderer;
    private float elapsedTime;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        elapsedTime = 0f;
    }

    private void Update() {
        // Add the time since the last frame to the elapsed time
        elapsedTime += Time.deltaTime;
        float t = elapsedTime * frequency;

        float value;
        if (sine) value = (Mathf.Sin(t * Mathf.PI * 2f) + 1f) / 2f; // sine wave
        else value = Mathf.Repeat(t, 1f) < 0.5f ? 0f : 1f; // square wave

        // Change the alpha value
        objectRenderer.material.color = new Color(1f, 1f, 1f, value);
    }

}
