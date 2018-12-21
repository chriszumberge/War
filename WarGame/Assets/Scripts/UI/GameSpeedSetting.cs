using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSetting : MonoBehaviour {

    public Image SlowButton;
    public Image NormalButton;
    public Image FastButton;
    public Image VeryFastButton;

	public void SetSlowSpeed()
    {
        ResetButtons();

        GlobalSettings.Instance.TimeMultiplier = 0.5f;
        SlowButton.color = SlowButton.color.MultiplyAlpha(1.0f);
    }

    public void SetNormalSpeed()
    {
        ResetButtons();

        GlobalSettings.Instance.TimeMultiplier = 1.0f;
        NormalButton.color = NormalButton.color.MultiplyAlpha(1.0f);
    }

    public void SetFastSpeed()
    {
        ResetButtons();

        GlobalSettings.Instance.TimeMultiplier = 1.5f;
        FastButton.color = FastButton.color.MultiplyAlpha(1.0f);
    }

    public void SetVeryFastSpeed()
    {
        ResetButtons();

        GlobalSettings.Instance.TimeMultiplier = 2.0f;
        VeryFastButton.color = VeryFastButton.color.MultiplyAlpha(1.0f);
    }

    void ResetButtons()
    {
        SlowButton.color = SlowButton.color.MultiplyAlpha(0);
        NormalButton.color = NormalButton.color.MultiplyAlpha(0);
        FastButton.color = FastButton.color.MultiplyAlpha(0);
        VeryFastButton.color = VeryFastButton.color.MultiplyAlpha(0);
    }
}