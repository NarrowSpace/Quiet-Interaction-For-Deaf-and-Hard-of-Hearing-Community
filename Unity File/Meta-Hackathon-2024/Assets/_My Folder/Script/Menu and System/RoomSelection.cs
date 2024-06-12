using UnityEngine;
using TMPro;

public class RoomSelection : MonoBehaviour
{
    public GameObject currentRoom = null; 
    public Animator roomAnim; 
    public GameObject[] rooms; 

    private void Start()
    {
        roomAnim = GetComponent<Animator>(); 
    }

    /*public void OnRoomSelected(GameObject selectedRoom)
    {
        currentRoom = selectedRoom; // Update the current room

        foreach (GameObject room in rooms)
        {
            bool isSelected = room == selectedRoom;

            switch (room.name)
            {
                case "KitchenCube":
                    roomAnim.SetBool("isKitchen", isSelected);
                    break;

                case "LivingRoom":
                    roomAnim.SetBool("isLivingroom", isSelected);
                    break;

                case "BathCube":
                    roomAnim.SetBool("isBathroom", isSelected);
                    break;
            }
        }

        // Single Trigger Use: used once all boolean values have been set. 
        // For clean transition between rooms before any other room can be selected.
        roomAnim.SetTrigger("BacktoEmpty");
    }*/

    public void OnRoomSelected(GameObject selectedRoom)
    {
        if (currentRoom == selectedRoom)
        {
            return;
        }
        currentRoom = selectedRoom;

        UpdateRoomAnimations(selectedRoom);
    }

    public void UpdateRoomAnimations(GameObject selectedRoom)
    {
        foreach (GameObject room in rooms)
        {
            bool isSelected = room == selectedRoom;
            switch (room.name)
            {
                case "KitchenCube":
                    roomAnim.SetBool("isKitchen", isSelected);
                    break;
                case "LivingRoom":
                    roomAnim.SetBool("isLivingroom", isSelected);
                    break;
                case "BathCube":
                    roomAnim.SetBool("isBathroom", isSelected);
                    break;
            }
        }

        roomAnim.SetTrigger("BacktoEmpty");
    }

    public void DeactivateCurrentRoom()
    {
        if (currentRoom != null)
        {
            switch (currentRoom.name)
            {
                case "KitchenCube":
                    roomAnim.SetBool("isKitchen", false);
                    break;
                case "LivingRoom":
                    roomAnim.SetBool("isLivingroom", false);
                    break;
                case "BathCube":
                    roomAnim.SetBool("isBathroom", false);
                    break;
            }

            roomAnim.SetTrigger("BacktoEmpty");
        }
    }

}
