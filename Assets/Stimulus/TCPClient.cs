using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class TCPClient : MonoBehaviour {
    [SerializeField] private string serverIP = "192.168.0.100";
    [SerializeField] private int serverPort = 42069;
    [SerializeField] private string startMessage = "meta";
    [SerializeField] private string endMessage = "end";

    private TcpClient client;
    private NetworkStream stream;
    private bool isConnected = false;
    private ExperimentManager experimentManager;
    private List<string> messageList = new List<string>();

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
            // create the TCP client and connect to the server
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();

            // send the start message
            byte[] data = Encoding.UTF8.GetBytes(startMessage);
            stream.Write(data, 0, data.Length);

            // wait for the response
            data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string response = Encoding.UTF8.GetString(data, 0, bytes);

            // if the response is the start message, start the experiment
            if (response == startMessage) {
                isConnected = true;
                data = Encoding.UTF8.GetBytes("Connected to server.");
                stream.Write(data, 0, data.Length);
                // start the first experiment
                SendTCP("Starting experiment 0.");
                experimentManager.StartExperimentManager();
            }

        } catch (SocketException ex) {
            Debug.LogError("Error connecting to server: " + ex.Message);
        }
    }


    public void SendTCP(string message) {
        // if not connected save the messages in an array and send them when connected
        if(!isConnected) {
            messageList.Add(message);
            return;
        }

        if (messageList.Count > 0) {
            foreach (string msg in messageList) {
                byte[] dataOld = Encoding.UTF8.GetBytes(msg+"\n");
                stream.Write(dataOld, 0, dataOld.Length);
            }
            messageList.Clear();
        }
        byte[] data = Encoding.UTF8.GetBytes(message+"\n");
        stream.Write(data, 0, data.Length);

    }

    private void CloseConnection() {
        isConnected = false;
        
        byte[] data = Encoding.UTF8.GetBytes(endMessage);
        stream.Write(data, 0, data.Length);

        stream.Close();
        client.Close();
        // experimentManager.SetExperiment(-1);
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
