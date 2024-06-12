using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using System.Collections;
using System;

public class TextToSpeech : MonoBehaviour
{
    // Azure Key and Region
    [SerializeField] private string KEY = "TYPE YOUR AZURE SPEECH KEY HERE";
    [SerializeField] private string REGION = "eastus";

    // [SerializeField] private GameObject ray;

    [SerializeField] private OVRVirtualKeyboard keyboard;
    [SerializeField] private TextMeshProUGUI _debug;

    [Header("Menu Position")]
    [SerializeField] private GameObject inputFieldAndKeyboard;

    public Camera centerCam;

    [SerializeField] private float yOffest = -0.25f;
    [SerializeField] private float zOffest = 0.5f;
    private float posSpeed = 2.0f;
    private float rotSpeed = 2.0f;

    private bool isKeyboardActive = false;

    // Receiving the state from the "LiveCaptionManager.cs"
    public void SetKeyboardState(bool state)
    {
        isKeyboardActive = state;

        if (isKeyboardActive)
        {
            keyboard.EnterEvent.AddListener(KeyboardEventTriggered); // Listen to 'If the keyboard has pressed Enter Key'
        }
        else
        {
            keyboard.EnterEvent.RemoveListener(KeyboardEventTriggered); // Remove listener when not active
        }
    }

    private void Update()
    {
        if (isKeyboardActive)
        {
            UpdateKeyboardPosition();
        }
    }

    private void UpdateKeyboardPosition()
    {
        Vector3 distance = inputFieldAndKeyboard.transform.position - centerCam.transform.position;
        Quaternion rot = Quaternion.LookRotation(distance);

        Vector3 position = centerCam.transform.position + centerCam.transform.rotation * new Vector3(0, yOffest, zOffest);

        // Lerp the position and rotation of the keyboard
        inputFieldAndKeyboard.transform.position = Vector3.Lerp(inputFieldAndKeyboard.transform.position, position, posSpeed * Time.deltaTime);
        inputFieldAndKeyboard.transform.rotation = Quaternion.Slerp(inputFieldAndKeyboard.transform.rotation, rot, rotSpeed * Time.deltaTime);

        inputFieldAndKeyboard.transform.localScale = Vector3.one * 0.9f;
    }

    private void KeyboardEventTriggered()
    {
        string fulltext = keyboard.TextHandler.Text;
        _debug.text = fulltext;
        StartCoroutine(TextSpeechCoroutine(fulltext));
    }

    private IEnumerator TextSpeechCoroutine(string text)
    {
        yield return Task.Run(() => SpeakToText(text));   // Wait for the task to complete
    }

    private async Task SpeakToText(string text)
    {
        var config = SpeechConfig.FromSubscription(KEY, REGION);
        config.SpeechSynthesisVoiceName = "en-US-AvaNeural";

        using (var synthesizer = new SpeechSynthesizer(config))
        {
            var result = await synthesizer.SpeakTextAsync(text);

        }
    }

}
