using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;

public class UDPReceive : MonoBehaviour
{

    Thread receiveThread;
    UdpClient client; 
    public int port = 5052;
    public bool printToConsole = false;
    public string data;

    private bool startRecieving = false;

    public void StartReceiving()
    {
        startRecieving = true; // Ensure the loop will run
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // Add a public method to stop the thread
    public void StopReceiving()
    {
        startRecieving = false;
        if (client != null)
        {
            client.Close();
        }
    }

    // receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);

        while (startRecieving)
        {

            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if (printToConsole) { print(data); }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    /*public void SendMessage()
    {
        if (startRecieving)
        {
            receivedText = data;
        }
    }*/


}
