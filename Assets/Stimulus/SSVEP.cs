using UnityEngine;
using System.Collections.Generic;

public class SSVEP : MonoBehaviour {
    [SerializeField] private float frequency;
    [SerializeField] private bool sine;
    [SerializeField] private bool sendData = false;

    private Renderer objectRenderer;
    private float elapsedTime;
    private List<float> valuesList;
    private TCPClient client;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        elapsedTime = 0f;
        valuesList = new List<float>();
        // if the frequency is 0, make hte object invisible
        if (frequency == 0f) objectRenderer.enabled = false;
        if(sendData) client = GetComponentInParent<TCPClient>();
    }

    private void Update() {
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
        // Save the value in the list
        valuesList.Add(value);

        if(elapsedTime>5 && sendData) SendDataTCP();
    }

    public void StopSSVEP(){
        objectRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SendDataTCP(){
        string data = "time: " + elapsedTime + "\n";
        foreach (float val in valuesList) {
            data += val + "\n";
        }
        client.SendTCP(data);
        sendData = false;
        frequency = 0f;
    }
}
