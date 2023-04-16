using UnityEngine;
using System.Collections.Generic;

public class SSVEP : MonoBehaviour {
    [SerializeField] private float frequency;
    [SerializeField] private bool sine;
    [SerializeField] private bool sendData = false;

    private Renderer objectRenderer;
    private float elapsedTime;
    private TCPClient client;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        elapsedTime = 0f;
        // if the frequency is 0, make the object invisible
        if (frequency == 0f) objectRenderer.enabled = false;
        if(sendData) client = GetComponentInParent<TCPClient>();
    }

    private void FixedUpdate() {
        // if the frequency is 0, do nothing
        if (frequency == 0f) return;
        // Add the time since the last frame to the elapsed time
        elapsedTime += Time.deltaTime;
        float t = elapsedTime * frequency;

        float value;
        if (sine) value = (Mathf.Sin(t * Mathf.PI * 2f) + 1f) / 2f; // sine wave
        else value = Mathf.Repeat(t, 1f) < 0.5f ? 0f : 1f; // square wave

        // Change the alpha value
        objectRenderer.material.color = new Color(1f, 1f, 1f, value);
    }

    public void StopSSVEP(){
        objectRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }

}
