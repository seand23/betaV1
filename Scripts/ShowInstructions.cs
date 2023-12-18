using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowInstructions : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;

    public void ToggleInstructions()
    {
        instructionsText.gameObject.SetActive(true);
        Debug.Log("instructions called for");
    }
}
