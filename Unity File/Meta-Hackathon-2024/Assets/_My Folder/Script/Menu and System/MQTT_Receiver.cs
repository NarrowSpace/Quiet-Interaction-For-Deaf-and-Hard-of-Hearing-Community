using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json.Linq;

// using System.Diagnostics;  

// This code is based & modified based on my needs: https://workshops.cetools.org/codelabs/CASA0019-unity-mqtt/#3

/// <summary>
/// This script needs to derive from the main class M2MqttUnityClient
/// </summary>
public class MQTT_Receiver: M2MqttUnityClient
{
    [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
    public bool autoTest = false;

    [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
    public bool isPulishing = false;

    [Tooltip("MQTT list tp Subscribe")]
    public List<string> topicToSubscribe = new List<string>();

    [Header("Subscribed Topic Controllers")]
    public DoorController doorController; // Motion Sensor
    public AirFryerHandler airFryerHandler;
    public MicrowaveHandler microwaveHandler;
    public WaterLevelHandler waterLevelHandler;
    public FridgeHandler fridgeHandler;

    [Header("Publish to Device")]
    [Tooltip("MQTT list to Publish to")]
    public List<string> topicToPublish = new List<string>();

    // List to store multiple messages from MQTT
    private List<string> eventMessage = new List<string>();

    private string m_msg;  // This variable will be used to store the message arrived from MQTT
    public string Msg 
    {
        get { return m_msg; }

        set
        {
            // Checks if the new value being assigned is the same as the current value
            if (m_msg == value) return; // If the values are the same, return and doing nothing

            m_msg = value;  // Assigns the new value to 'm_msg'

            // Checks if the 'OnMsgArrived' event has any subscribers
            // If it does, the event is raised with 'm_msg' as its argument
            if (OnMsgArrived != null) 
            {
                OnMsgArrived(m_msg);
            } 
        }
    }

    // This delegate can point to any method that takes a single string parameter and Do not return anything
    public delegate void OnMsgArrivedDelegate(string newMsg); // Handling incoming messages
    public event OnMsgArrivedDelegate OnMsgArrived;

    private bool m_isConnected;

    public bool isConnected
    {
        get { return m_isConnected; }

        set
        {
            if (m_isConnected == value) return;

            m_isConnected = value;

            if (OnConnectedChanged != null) OnConnectedChanged(isConnected);
        }
    }

    public delegate void OnConnectionSucceededDelegate(bool isConnected);
    public event OnConnectionSucceededDelegate OnConnectedChanged;

    public void Publish(int index, string msg)
    {
        if (index >= 0 && index < topicToPublish.Count)
        {
            string topic = topicToPublish[index];
            string message = msg;
            client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

    }

    /// <summary>
    /// The following code is based on the M2MqttUnityClient class
    /// Using protected and override to tailor the class to our needs
    /// </summary>
    public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        isConnected = true;
        Debug.Log("Connected to broker on " + brokerAddress);
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("Connection Failed due to" + errorMessage);
    }

    protected override void OnDisconnected()
    {
        base.OnDisconnected();
        Debug.Log("Disconnected from broker on " + brokerAddress);
        isConnected = false;
    }

    protected override void OnConnectionLost()
    {
        base.OnConnectionLost();
        isConnected = false;
    }

    protected override void SubscribeTopics()
    {

        // Subscribe to each topic in the list
        foreach (string topicToSubscribe in topicToSubscribe)
        {
            client.Subscribe(new string[] { topicToSubscribe }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            Debug.Log("successfully Subscribed to topic: " + topicToSubscribe);
        }

    }

    protected override void UnsubscribeTopics()
    {
        foreach (string topicSubscribe in topicToSubscribe)
        {
            client.Unsubscribe(new string[] { topicSubscribe });
            Debug.Log("Unsubscribed to topic: " + topicToSubscribe);
        }

    }

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Message is received and decoded in the following method
    /// </summary>
    protected override void DecodeMessage(string topic, byte[] message)
    {
        Msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log("Received: " + Msg);
        Debug.Log("from Topic: " + topic + m_msg);

        int index = topicToSubscribe.IndexOf(topic);
        var data = JObject.Parse(Msg); // Parse the JSON message

        switch (index)
        {
            case 0:
                doorController.DoorHandler(data);
                break;
            case 1:
                airFryerHandler.AirFryerState(data);
                break;
            case 2:
                microwaveHandler.MicrowaveState(data);
                break;
            case 3:
                waterLevelHandler.WaterLevel(data);
                break;
            case 4:
                fridgeHandler.FridgeState(data);
                break;
        }


        // Check the index of the topic 0 has to subscribe to the door sensor in the inspector
        if (index == 0)
        {
            doorController.DoorHandler(data);
            Debug.Log("Send data to Door Handler");
        }

        StoreMessage(Msg);
    }

    private void StoreMessage(string eventMsg)
    {
        if (eventMessage.Count > 50)
        {
            eventMessage.Clear();
        }
        eventMessage.Add(eventMsg);
    }

    protected override void Update()
    {
        base.Update(); 

    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
        if (autoTest)
        {
            autoConnect = true;
        }
    }
}
