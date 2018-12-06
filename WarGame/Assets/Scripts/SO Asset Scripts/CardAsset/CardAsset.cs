﻿using UnityEngine;

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("General info")]
    //[TextArea(2,3)]
    //public string Description;  // Description for spell or character
    public FactionAsset FactionAsset;
	public Sprite CardImage;
    [Range(0, 120)]
    public int Value;
}