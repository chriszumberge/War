using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [Header("Players")]
    public Player TopPlayer;
    public Player LowPlayer;
    [Header("Colors")]
    [Header("Numbers and Values")]
    [Header("Prefabs and Assets")]
    public GameObject CreatureCardPrefab;
    //public GameObject LightningEffectPrefab;
    [Header("Other")]
    public GameObject GameOverCanvas;
    //public Sprite HeroPowerCrossMark;

    // SINGLETON
    public static GlobalSettings Instance;

    void Awake()
    {
        Instance = this;
    }
}
