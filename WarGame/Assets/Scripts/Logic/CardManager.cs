using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public List<CardAsset> DepthCards = new List<CardAsset>();
    public List<CardAsset> DivineCards = new List<CardAsset>();
    public List<CardAsset> PrimalCards = new List<CardAsset>();
    public List<CardAsset> UndeadCards = new List<CardAsset>();
    public List<CardAsset> WarriorCards = new List<CardAsset>();

    public List<CardAsset> SpecialCards = new List<CardAsset>();

    public static CardManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
