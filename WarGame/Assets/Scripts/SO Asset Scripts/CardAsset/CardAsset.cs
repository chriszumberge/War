using UnityEngine;

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("General info")]
    //[TextArea(2,3)]
    //public string Description;  // Description for spell or character
    public FactionAsset FactionAsset;
	public Sprite CardImage;
    [Range(0, 140)]
    public int Value;

    // Using asset name as title right now
    //public string Title;
    public string Subtitle;

    // If we want like snippets about cards
    public string Description;
}
