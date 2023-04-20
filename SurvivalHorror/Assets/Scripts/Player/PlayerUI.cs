using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptText;

    private void Start()
    {
        
    }

    public void UpdateText(string promptMessage)
    {
        _promptText.text = promptMessage;
    }
}
