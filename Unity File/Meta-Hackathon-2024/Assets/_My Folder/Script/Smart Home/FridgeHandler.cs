using Newtonsoft.Json.Linq;
using UnityEngine;

// Urgent Event: Fridge Door Open
public class FridgeHandler : MonoBehaviour
{
    [SerializeField] private Animator fridgeAnim;
    [SerializeField] private Material fridgeMat;
    [SerializeField] private GameObject toolTip;

    [Header("Room Anim")]
    [SerializeField] private RoomSelection roomSelection;
    [SerializeField] private GameObject kitchenCube;

    private void Start()
    {
        fridgeMat.SetColor("_BaseColor",new Color32(188,188,188,255)); // Grey color
        toolTip.SetActive(false);
    }

    public void FridgeState(JObject data)
    {
        bool state = data["contact"].Value<bool>();

        if (!state)
        {
            if (roomSelection.currentRoom != kitchenCube)
            {
                roomSelection.OnRoomSelected(kitchenCube);
            }
        }

        fridgeAnim.SetBool("isOpen", !state); //False means door is open

        ChangeColor(state);

        toolTip.SetActive(!state);
    }

    public void ChangeColor(bool state)
    {
        // Set color based on the state: specific grey if true, specific red if false
        Color color = state ? new Color32(188, 188, 188, 255) : new Color32(173, 36, 36, 255);

        fridgeMat.SetColor("_BaseColor", color);
    }

}
