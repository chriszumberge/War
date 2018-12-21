using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SettingsButton : MonoBehaviour
{
    public GameObject SettingsWindow;

    Image _selfImage;
    private void Awake()
    {
        _selfImage = this.GetComponent<Image>();
    }
    public void OnClicked()
    {
        SettingsWindow.SetActive(!SettingsWindow.activeSelf);

        if (SettingsWindow.activeSelf)
        {
            _selfImage.color = Color.black;
        }
        else
        {
            _selfImage.color = new Color(91 / 255.0f, 91 / 255.0f, 91 / 255.0f);
        }
    }
}
