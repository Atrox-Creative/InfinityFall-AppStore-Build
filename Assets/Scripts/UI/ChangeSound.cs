using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeSound : MonoBehaviour
{
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;
    private bool isOn;

    void Start()
    {
        isOn = (PlayerPrefs.GetInt("Name") != 0);

        if (isOn)
        {
            button.image.sprite = soundOffImage;
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void ButtonClicked()
    {
        isOn = (PlayerPrefs.GetInt("Name") != 0);

        if (!isOn)
        {
            print("muteof");
            button.image.sprite = soundOffImage;
            PlayerPrefs.SetInt("Name", (isOn ? 0 : 1));            

            AudioListener.volume = 0;
        }
        else
        {
            print("muteon");
            button.image.sprite = soundOnImage;
            PlayerPrefs.SetInt("Name", (isOn ? 0 : 1));

            AudioListener.volume = 1;
        }
    }
}
