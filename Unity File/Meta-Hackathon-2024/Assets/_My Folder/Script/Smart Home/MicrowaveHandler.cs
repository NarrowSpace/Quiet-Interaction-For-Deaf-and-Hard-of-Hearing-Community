using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

// Non-Urgent Event: Microwave Cycle Completed
public class MicrowaveHandler : MonoBehaviour
{
    [Header("Mircowave")]
    [SerializeField] private Animator microwaveAnim;
    [SerializeField] private Material microwaveMat;

    [Header("Menu")]
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Button checkMark;

    private bool isMicrowaveOn = false;
    private bool isCycleCompleted = false;

    [Header("Room Animation")]
    [SerializeField] private RoomSelection roomSelection;
    [SerializeField] private GameObject kitchenCube;

    private void Start()
    {
        microwaveMat.SetColor("_BaseColor", new Color32(188, 188, 188, 255));

        toolTip.SetActive(false);

        checkMark.onClick.AddListener(TurnOffCanvas);
    }

    public void MicrowaveState(JObject data)
    {
        float instantPower = data["power"].Value<float>();

        if(instantPower > 1000)
        {
            // Debug.Log("isMicrowaveOn = true");
            isMicrowaveOn = true;
        }

        if (isMicrowaveOn && instantPower < 10)
        {
            if (roomSelection.currentRoom != kitchenCube)
            {
                roomSelection.OnRoomSelected(kitchenCube);
            }

            // Debug.Log("Cycle Completed");
            microwaveAnim.SetBool("isReady", true);

            microwaveMat.SetColor("_BaseColor", new Color32(105, 237, 126, 255));

            toolTip.SetActive(true);

            isMicrowaveOn = false;

            isCycleCompleted = true;
        }

    }

    private void TurnOffCanvas()
    {
        if (isCycleCompleted)
        {
            microwaveAnim.SetBool("isReady", false);

            microwaveMat.SetColor("_BaseColor", new Color32(188, 188, 188, 255));

            toolTip.SetActive(false);

            isCycleCompleted = false;
        }
    }


}
