using UnityEngine;
using System.Collections.Generic;

public class SSVEP : MonoBehaviour {
    [SerializeField] private float frequency;
    [SerializeField] private bool sine;
    [SerializeField] private string fileName = "savedData.txt";
    [SerializeField] private bool sendData = false;

    private Renderer objectRenderer;
    private float elapsedTime;
    private List<float> valuesList;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        elapsedTime = 0f;
        valuesList = new List<float>();
        // if the frequency is 0, make hte object invisible
        if (frequency == 0f) objectRenderer.enabled = false;
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
    }

    public void StopSSVEP(){
        objectRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }

    // Function to retrieve the list of values
    public List<float> GetValuesList() {
        return valuesList;
    }

    private void OnApplicationQuit()
    {
        //if saveData is true, save the data from valuesList locally
        if (sendData) {
            string path = Application.dataPath + "/" + fileName;
            string data = "";
            foreach (float value in valuesList) {
                data += value + "\n";
            }
            System.IO.File.WriteAllText(path, data);
        }
    }
}
