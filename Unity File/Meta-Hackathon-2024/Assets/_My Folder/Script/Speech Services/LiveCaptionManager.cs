using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class LiveCaptionManager : MonoBehaviour
{
    [Header("Text-to-Speech services menu")]
    // [SerializeField] private GameObject _startMenu;
    // [SerializeField] private GameObject _liveCaption;
    [SerializeField] private GameObject _textToSpeech;

    private bool micPermissionGranted = false;

    private TextToSpeech keyboardOn;
    public UDPReceive udpReceive;

    private bool isLiveCaptionOn = false;

    private void Awake()
    {
        // Get the micrphone permission on Android
#if PLATFORM_ANDROID
        // Check and request microphone permission on Android
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        // Proceed only if microphone permission is granted
        micPermissionGranted = Permission.HasUserAuthorizedPermission(Permission.Microphone);
#endif

        // _liveCaption.SetActive(false);
        _textToSpeech.SetActive(false);
        keyboardOn = _textToSpeech.GetComponent<TextToSpeech>();
    }

    public void TurnOnLiveCaption(bool isCaption)
    {
        isLiveCaptionOn = isCaption;

        // _startMenu.SetActive(false);
        // _liveCaption.SetActive(true);

        if (micPermissionGranted)
        {
            udpReceive.StartReceiving();
        }
        else
        {
            // info.text = "Microphone permission is not granted";
            return;
        }

    }

    public void TurnOnTextToSpeech()
    {
        if (isLiveCaptionOn)
        {
            _textToSpeech.SetActive(true);
            keyboardOn.SetKeyboardState(true);
           
        }
    }
}
