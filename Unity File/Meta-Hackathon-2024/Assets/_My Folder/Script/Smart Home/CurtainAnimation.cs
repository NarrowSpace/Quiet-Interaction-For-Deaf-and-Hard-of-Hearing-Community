using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainAnimation : MonoBehaviour
{
    public List<Transform> blindToRotate = new List<Transform>(); // Assign these in the inspector

    private float minRot = 0;
    private float maxRot = 80;

   public void BlindRotation(float sliderValue)
   {
        float targetRotationY = Mathf.Lerp(minRot, maxRot, sliderValue / 100f);

        if (blindToRotate.Count > 0)
        {
            Transform leadBlind = blindToRotate[0];

            leadBlind.localRotation = Quaternion.Euler(0, targetRotationY, 0);

            for (int i = 1; i < blindToRotate.Count; i++)
            {
                Transform blind = blindToRotate[i];

                if (blind != null)
                {
                    blind.localRotation = leadBlind.localRotation;
                }
            }
        }
    }
}
