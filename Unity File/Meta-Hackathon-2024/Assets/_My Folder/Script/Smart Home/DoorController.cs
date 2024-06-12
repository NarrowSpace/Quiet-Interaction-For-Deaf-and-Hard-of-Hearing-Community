using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Non-Urgent Event: Door Open
public class DoorController : MonoBehaviour
{
    // Main Door Model
    [Header("Door")]
    [SerializeField] private Animator doorAnim;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Button checkMark;

    [Header("Character")]
    // Main Charatcer Interaction
    [SerializeField] private GameObject character;
    [SerializeField] private Animator chaAnim;

    [Header("Room Animation")]
    [SerializeField] private RoomSelection roomSelection;
    [SerializeField] private GameObject livingroomCube;

    private void Start()
    {
        toolTip.SetActive(false);
        character.SetActive(false);
        checkMark.onClick.AddListener(TurnOffCanvas);
    }

    public void DoorHandler(JObject data)
    {
        bool state = data["occupancy"].Value<bool>();  //Detected = True, Not Detected = False

        if (state)
        {
            toolTip.SetActive(true);

            if (roomSelection.currentRoom != livingroomCube)
            {
                roomSelection.OnRoomSelected (livingroomCube);
            }

            doorAnim.SetBool("isDoorOpen", true);
            StartCoroutine(WaitTilAnimFinish("isDoorOpen", 0));
        }
    }

    private IEnumerator WaitTilAnimFinish(string animationName, int layer)
    {
        yield return new WaitForEndOfFrame(); // Wait for the animation to start

        AnimatorStateInfo stateInfo = doorAnim.GetCurrentAnimatorStateInfo(layer);

        // Wait for the duration of the animation
        float waitTime = stateInfo.length;
        yield return new WaitForSeconds(waitTime);

        character.SetActive(true);
        chaAnim.SetBool("isMotion", true);
    }

    private void TurnOffCanvas()
    {
        toolTip.SetActive(false);

        character.SetActive(false);

        doorAnim.SetBool("isDoorOpen", false);
    }

}
