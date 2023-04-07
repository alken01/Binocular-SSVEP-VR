using UnityEngine;

public class SSVEPBlock : MonoBehaviour {
    [SerializeField] private float frequency;
    [SerializeField] private bool sine;
    
    private Renderer objectRenderer;
    private float elapsedTime;
    private bool isOn;
    private TCPClient client;
    private bool isPaused;
    private PauseSSVEP pause;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        elapsedTime = 0f;

        // The parent of the parent contains the TCPClient script
        client = transform.parent.parent.GetComponent<TCPClient>();
        isOn = CheckConnection();
        pause = transform.parent.parent.GetComponent<PauseSSVEP>();
        isPaused = CheckPaused();
    }

    private void Update() {
        // If frequency is 0, static image
        if (frequency == 0f) return;

        // if the connection is not on or the block is paused, set the alpha value to 1
        if (!CheckConnection() || CheckPaused()) {
            objectRenderer.material.color = new Color(1f, 1f, 1f, 1f);
            return;
        }
        
        // else, check for conn adn update the alpha value
        CheckConnection();
        CheckPaused();
        UpdateAlpha();
    }

    private bool CheckConnection() {
        isOn = client.GetConnectionStatus();
        return isOn;
    }

    private bool CheckPaused(){
        isPaused = pause.GetPauseStatus();
        return isPaused;
    }

    private void UpdateAlpha() {
        // Add the time since the last frame to the elapsed time
        elapsedTime += Time.deltaTime;
        float t = elapsedTime * frequency;
        float value = sine ? (Mathf.Sin(t * Mathf.PI * 2f) + 1f) / 2f : Mathf.Repeat(t, 1f) < 0.5f ? 0f : 1f;
        
        // Change the alpha value
        objectRenderer.material.color = new Color(1f, 1f, 1f, value);
    }

}
