using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

// Non-Urgent Event: Air Fryer Cycle Completed
public class AirFryerHandler : MonoBehaviour
{
    [Header("Air fryer")]
    [SerializeField] private Animator airFryerAnim;
    [SerializeField] private Material airFryerMat;

    [Header("Menu")]
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Button checkMark;

    private bool isAirFryerOn = false;
    private bool isCycleCompleted = false;

    [Header("Room Animation")]
    // Reference to the RoomSelection script on the kitchen object
    [SerializeField] private RoomSelection roomSelection;
    [SerializeField] private GameObject kitchenCube; 

    private void Start()
    {
        airFryerMat.SetColor("_BaseColor", new Color32(188, 188, 188, 255));
        toolTip.SetActive(false);
        checkMark.onClick.AddListener(TurnOffCanvas);
    }

    public void AirFryerState(JObject data)
    {
        float instantPower = data["power"].Value<float>();
        // Debug.Log("Airfryer instant POWER = " + instantPower);

        if (instantPower > 200)
        {
            // Debug.Log("isAirFryerReady = true");
            isAirFryerOn = true;
        }

        if (isAirFryerOn && instantPower < 10)
        {
            // Debug.Log("Cycle Completed");

            if (roomSelection.currentRoom != kitchenCube)
            {
                roomSelection.OnRoomSelected(kitchenCube);
            }

            airFryerAnim.SetBool("isAirFryerReady", true);

            airFryerMat.SetColor("_BaseColor", new Color32(105,237,126, 255));

            toolTip.SetActive(true);

            isAirFryerOn = false;
            isCycleCompleted = true;
        }
    }

    private void TurnOffCanvas()
    {
        if (isCycleCompleted)
        {
            airFryerAnim.SetBool("isAirFryerReady", false);

            airFryerMat.SetColor("_BaseColor", new Color32(188, 188, 188, 255));

            toolTip.SetActive(false);

            isCycleCompleted = false;
        }
    }
}
