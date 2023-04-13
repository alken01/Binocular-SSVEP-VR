using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Collections;

public class TCPClient : MonoBehaviour {
    [SerializeField] private string serverIP = "192.168.0.100";
    [SerializeField] private int serverPort = 42069;
    [SerializeField] private string startMessage = "meta";
    [SerializeField] private string endMessage = "end";

    private TcpClient client;
    private NetworkStream stream;
    private bool isConnected = false;
    private ExperimentManager experimentManager;

    private void Start() {
        experimentManager = GetComponent<ExperimentManager>();
    }

    private void Update() {
        // R2 button to connect
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && !isConnected) {
            ConnectToServer();
        }

        // A button to disconnect
        if (OVRInput.GetDown(OVRInput.Button.One) && isConnected) {
            CloseConnection();
        }
    }

    private void ConnectToServer() {
        try {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            SendTCP(startMessage);

            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string response = Encoding.UTF8.GetString(data, 0, bytes);
            if (response == startMessage) {
                isConnected = true;
                SendTCP("Connected to Server.");
                experimentManager.SetExperiment(0);
            }
        } catch (SocketException ex) {
            Debug.LogError("Error connecting to server: " + ex.Message);
        }
    }

    public void SendTCP(string message) {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    private void CloseConnection() {
        isConnected = false;
        SendTCP(endMessage);
        stream.Close();
        client.Close();
        experimentManager.SetExperiment(-1);
    }

    public bool GetConnectionStatus() {
        return isConnected;
    }

    private void OnDestroy() {
        if (isConnected) {
            CloseConnection();
        }
    }

    private void OnApplicationQuit() {
        if (isConnected) {
            CloseConnection();
        }
    }
}
