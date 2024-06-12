using UnityEngine;
using Newtonsoft.Json.Linq;

// Urgent Event: Water Leak
public class WaterLevelHandler : MonoBehaviour
{
    [SerializeField] private Animator waterLevelAnim;
    [SerializeField] private Material waterLevelMat;
    [SerializeField] private GameObject toolTip;
    // [SerializeField] private Button checkMark;

    [Header("Room Animation")]
    [SerializeField] private RoomSelection roomSelection;
    [SerializeField] private GameObject bathCube;

    private void Start()
    {
        waterLevelMat.SetColor("_BaseColor", new Color32(124, 178, 255, 255));
        toolTip.SetActive(false);
    }

    public void WaterLevel(JObject data)
    {
        bool state = data["water_leak"].Value<bool>();

        if (state) 
        {
            waterLevelAnim.SetBool("isLeak", state);
            ChangeColor(state);
            toolTip.SetActive(state);

            if (roomSelection.currentRoom != bathCube)
            {
                roomSelection.OnRoomSelected(bathCube);
            }
        }

    }

    public void ChangeColor(bool state)
    {
        // Set color based on the state: BLUE if false, red if true
        Color color = state ? new Color32(173, 36, 36, 255) : new Color32(124, 178, 255, 255);
        waterLevelMat.SetColor("_BaseColor", color);
    }
}
