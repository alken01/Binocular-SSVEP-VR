using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class TCPClient : MonoBehaviour {
    [SerializeField] private string serverIP = "192.168.0.100";
    [SerializeField] private int serverPort = 42069;
    [SerializeField] private string startMsg = "meta";
    [SerializeField] private string endMsg = "end";

    private TcpClient client;
    private NetworkStream stream;
    private bool isConnected = false;

    private void Update() {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
            if (!isConnected) {
                ConnectToServer();
            } else {
                SendTCP("PrimaryIndexTrigger");
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) {
            isConnected = false;
            SendTCP(endMsg);
        }

    }

    private void ConnectToServer() {
        try {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            SendTCP(startMsg);

            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string response = Encoding.UTF8.GetString(data, 0, bytes);
            if (response == startMsg) {
                isConnected = true;
            }
        } catch (SocketException ex) {
            Debug.LogError("Error connecting to server: " + ex.Message);
        }
    }

    public void SendTCP(string message) {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    private void OnApplicationQuit() {
        SendTCP(endMsg);
        stream.Close();
        client.Close();
    }

    public bool GetConnectionStatus() {
        return isConnected;
    }
}
