using TMPro;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject UIpages;
    
    public UDPReceive udp;

    public TextMeshProUGUI receivedText;

    private bool isWebVisible = false;

    private string lastData = "";

    private void Start()
    {
        UIpages.SetActive(false);
    }

    private void Update()
    {
        if (isWebVisible && udp.data != lastData)
        {
            receivedText.text = udp.data;
        }
    }

    public void TurnOnWebView()
    {
        isWebVisible = !isWebVisible;

        UIpages.SetActive(isWebVisible);

        if (isWebVisible)
        {
            udp.StartReceiving();
        }
        else
        {
            udp.StopReceiving();
        }

    }
}
