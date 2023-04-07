using UnityEngine;

public class PauseSSVEP : MonoBehaviour {
    [SerializeField] private string startMsg = "start";
    [SerializeField] private string pauseMsg = "pause";
    [SerializeField] private string resumeMsg = "resume";
    [SerializeField] private float pauseDelay = 6f;

    private bool isPaused;
    private float elapsedTime;
    private TCPClient client;

    private void Start() {
        // The parent of the parent contains the TCPClient script
        client = GetComponent<TCPClient>();
        SendTCP(startMsg);
        elapsedTime = 0f;
        isPaused = false;
    }

    private void Update() {
        if (!client.GetConnectionStatus()) return;
        
        if(!isPaused) elapsedTime += Time.deltaTime;

        // after elapsedTime seconds stop the ssvep
        if (elapsedTime >= pauseDelay && !isPaused) {
            isPaused = true;
            SendTCP(pauseMsg);
            // SetBlocksPaused(isPaused);
            elapsedTime = 0f;
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) {
            isPaused = !isPaused;
            SendTCP(isPaused ? pauseMsg : resumeMsg);
            // SetBlocksPaused(isPaused);
            elapsedTime = 0f;
        }
    }

    public bool GetPauseStatus(){
        return isPaused;
    }

    // private void SetBlocksPaused(bool paused) {
    //     // Loop through all child objects of the main object
    //     foreach (Transform child in transform) {
    //         // Loop through all child objects of the child object
    //         foreach (Transform grandchild in child) {
    //             // Get the SSVEPBlock component from the grandchild object
    //             SSVEPBlock block = grandchild.GetComponent<SSVEPBlock>();
    //             if (block != null) {
    //                 // Pause or resume the SSVEP block
    //                 block.SetPaused(paused);
    //             }
    //         }
    //     }
    // }

    private void SendTCP(string message) {
        if (client.GetConnectionStatus()) {
            client.SendTCP(message);
        }
    }

    // private void OnApplicationQuit() {
    //     SendTCP("end");
    // }
}
