using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    
    [SerializeField] private TextMeshProUGUI _performedMovesText;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void UpdatePerformedMoves(int value)
    {
        _performedMovesText.text = "Performed Moves: " + value;
    }

    public void ReturnToMainMenu()
    {
        CharacterSelected._instance.ReturnToMainMenu();
    }
}
