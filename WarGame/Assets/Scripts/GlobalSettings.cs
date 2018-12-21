using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [Header("Player Defined Settings")]
    //[SteppedRange(0.25f, 2.0f, 0.25f)]
    public float TimeMultiplier = 1.0f;

    [Header("Default Settings")]
    public float StartGameMessageDisplayTime = 2.0f;
    public static float MessageStartTime { get { return GlobalSettings.Instance.StartGameMessageDisplayTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float WarMessageDisplayTime = 1.5f;
    public static float MessageWarTime { get { return GlobalSettings.Instance.WarMessageDisplayTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float GameOverMessageDisplayTime = 5.0f;
    public static float MessageGameEndTime { get { return GlobalSettings.Instance.GameOverMessageDisplayTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float DrawCardMoveTime = 0.5f;
    public static float CardMoveTime { get { return GlobalSettings.Instance.DrawCardMoveTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float DrawWarCardMoveTime = 0.25f;
    public static float WarCardMoveTime { get { return GlobalSettings.Instance.DrawWarCardMoveTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float FlipCardUpTime = 0.5f;
    public static float FlipUpTime { get { return GlobalSettings.Instance.FlipCardUpTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float FlipCardDownTime = 0.2f;
    public static float FlipDownTime { get { return GlobalSettings.Instance.FlipCardDownTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float DelayAfterCardRevealTime = 0.5f;
    public static float RevealDelayTime { get { return GlobalSettings.Instance.DelayAfterCardRevealTime / GlobalSettings.Instance.TimeMultiplier; } }

    public float DelayBetweenWarStackTime = 0.25f;
    public static float WarStackDelayTime { get { return GlobalSettings.Instance.DelayBetweenWarStackTime / GlobalSettings.Instance.TimeMultiplier; } }

    public static GlobalSettings Instance;

    private void Awake()
    {
        Instance = this;
    }
}
