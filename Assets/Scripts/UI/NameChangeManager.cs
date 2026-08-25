using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using static APIHandler;

public class NameChangeManager : MonoBehaviour
{
    public TextMeshProUGUI currentName;
    public TMP_InputField textInput;
    public TextMeshProUGUI textError;

    void OnEnable()
    {
        textInput.text = String.Empty;
        HideError();
        UpdateName();
    }

    void ShowError(string msg) { 
        textError.text = msg;
    }

    void HideError()
    {
        textError.text = string.Empty;
    }

    public void UpdateName()
    {
        currentName.text = GameManager.singleton.player.name;
    }

    public async void NameUpdate()
    {
        HideError();
        if (textInput.text.Length > 2)
        {
            try
            {
                // update it on server
                await UpdatePlayer(textInput.text, GameManager.singleton.player.token);

                // update it on local
                GameManager.singleton.UpdateName(textInput.text);
                textInput.text = String.Empty;
             }
            catch (Exception)
            {
                ShowError("Name already used or invalid.");
            }
        }
        else ShowError("Name must be at least 3 chars long");

    }
}
