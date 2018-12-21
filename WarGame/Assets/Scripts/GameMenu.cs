using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public Button StartGameButton;
    public Text StartGameButtonText;

    public List<Toggle> FactionToggles = new List<Toggle>();

    private void Update()
    {
        if (FactionToggles.Count(x => x.isOn) != 4)
        {
            StartGameButton.interactable = false;
            StartGameButtonText.color = StartGameButtonText.color.MultiplyAlpha(80.0f / 255.0f);
        }
        else
        {
            StartGameButton.interactable = true;
            StartGameButtonText.color = StartGameButtonText.color.MultiplyAlpha(1.0f);
        }
    }

    public void StartGameWithSettings()
    {
        GlobalSettings.Instance.Reset();

        foreach (Toggle factionToggle in FactionToggles)
        {
            if (factionToggle.isOn)
            {
                if (factionToggle.name.StartsWith("Depth"))
                {
                    GlobalSettings.Instance.UseDepths = true;
                }
                else if (factionToggle.name.StartsWith("Divine"))
                {
                    GlobalSettings.Instance.UseDivine = true;
                }
                else if (factionToggle.name.StartsWith("Primal"))
                {
                    GlobalSettings.Instance.UsePrimal = true;
                }
                else if (factionToggle.name.StartsWith("Undead"))
                {
                    GlobalSettings.Instance.UseUndead = true;
                }
                else if (factionToggle.name.StartsWith("Warrior"))
                {
                    GlobalSettings.Instance.UseWarrior = true;
                }
            }
        }

        GameManager.Instance.ResetGame();
    }
}
