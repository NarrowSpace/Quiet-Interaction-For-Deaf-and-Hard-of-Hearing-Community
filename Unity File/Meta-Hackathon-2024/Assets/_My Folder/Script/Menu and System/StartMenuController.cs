using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    [Header("Start Menu")]
    [SerializeField] private Transform _leftHandPos;
    [SerializeField] private Transform _startMenu;
    [SerializeField] private Camera _centerCam;

    [Header("Start Menu Tooltips")]
    [SerializeField] private GameObject _houseTooltip;
    [SerializeField] private GameObject _captionTooltip;

    [Header("Smart Home Scene")]
    [SerializeField] private GameObject _myRoomScene;

    [Header("Function Anims")]
    [SerializeField] private Animator _houseAnimator;
    [SerializeField] private Animator _captionAnimator;

    [Header("Speech Service Scene")]
    [SerializeField] private LiveCaptionManager startSpeechService;
    [SerializeField] private GameObject _liveCaptionScene;

    private float _posSpeed = 4.0f;
    private float _rotSpeed = 4.0f;

    private float _yOffest = 0.05f;
    private float _zOffest = 0;

    private float _xRot = - 15f;

    private bool isRoomSceneOpen = false;
    private bool isSpeechSceneOpen = false;

    private void Awake()
    {
        _startMenu.gameObject.SetActive(false);
        _houseTooltip.SetActive(false);
        _captionTooltip.SetActive(false);
        _liveCaptionScene.SetActive(false);
    }

    private void Update()
    {
       if(_startMenu != null)
        {
            HouseMovements(_yOffest, _zOffest);
        }
    }

    private void HouseMovements(float y, float z)
    {
        Vector3 distance = _startMenu.position - _centerCam.transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance);

        // Add x offset to the rotation
        Quaternion xRot = Quaternion.Euler(_xRot, 0, 0);
        Quaternion finalRot = rotation * xRot;

        _startMenu.rotation = Quaternion.Slerp(_startMenu.rotation, finalRot, _rotSpeed * Time.deltaTime);

        Vector3 yOffest = new Vector3(0, y, z);
        Vector3 position = _leftHandPos.position + yOffest;

        _startMenu.position = Vector3.Lerp(_startMenu.position, position, _posSpeed * Time.deltaTime);
    }

    // Turn on Start Menu
    public void TurnOnStartMenu()
    {
        _startMenu.gameObject.SetActive(true);
    }

    // Turn off Start Menu
    public void TurnOffStartMenu()
    {
        _startMenu.gameObject.SetActive(false);
    }

    // Change the size when hovering over the house
    public void HouseSize()
    {
        _houseAnimator.SetTrigger("hover");
        _houseTooltip.SetActive(true);
    }

    public void UnhoverHouseSize()
    {
        _houseAnimator.SetTrigger("unhover");
        _houseTooltip.SetActive(false);
    }

    public void TurnOnMyRoom()
    {
        _houseAnimator.SetTrigger("unhover");
        StartCoroutine(StartMyRoom("unhover",0));
    }

    // Activate Room Scene after the animation
    private IEnumerator StartMyRoom(string animationName, int layer)
    {
        yield return new WaitForEndOfFrame(); // Wait for the animation to start

        // Retrieve information about the current Animator state
        AnimatorStateInfo stateInfo = _houseAnimator.GetCurrentAnimatorStateInfo(layer);

        // Wait for the duration of the animation
        float waitTime = stateInfo.length;
        yield return new WaitForSeconds(waitTime);

        isRoomSceneOpen = !isRoomSceneOpen;

        _myRoomScene.SetActive(isRoomSceneOpen);

        _startMenu.gameObject.SetActive(false);

    }

    // Change the size when hovering over the caption
    public void CaptionSize()
    {
        _captionAnimator.SetTrigger("hover");
        _captionTooltip.SetActive(true);
    }

    public void UnhoverCaptionSize()
    {
        _captionAnimator.SetTrigger("unhover");
        _captionTooltip.SetActive(false);
    }

    public void TurnOnLiveCaptionMenu()
    {
        _captionAnimator.SetTrigger("unhover");
        StartCoroutine(StartLiveCaption("unhover", 0));
    }

    // Activate Live Caption after the animation
    private IEnumerator StartLiveCaption(string animtionName, int layer)
    {
        yield return new WaitForEndOfFrame(); // Wait for the animation to start

        // Retrieve information about the current Animator state
        AnimatorStateInfo stateInfo = _captionAnimator.GetCurrentAnimatorStateInfo(layer);

        // Wait for the duration of the animation
        float waitTime = stateInfo.length;
        yield return new WaitForSeconds(waitTime);

        isSpeechSceneOpen = !isSpeechSceneOpen;
        _liveCaptionScene.SetActive(isSpeechSceneOpen);
        startSpeechService.TurnOnLiveCaption(isSpeechSceneOpen);

        // Deactivate the Start Menu
        _startMenu.gameObject.SetActive(false);
    }

}
