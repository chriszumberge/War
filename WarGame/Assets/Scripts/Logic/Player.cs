using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour //, ICharacter
{
    public int PlayerID;
    //public PlayerArea PArea;

    public Deck deck;
    public Discard discard;
    public InPlay inPlay;
    public Table table;

    public CardAsset CardInPlay;
    public GameObject CardOnTable;

    public bool HasLost = false;

    public int ID
    {
        get{ return PlayerID; }
    }

    public Player otherPlayer
    {
        get
        {
            if (Players[0] == this)
                return Players[1];
            else
                return Players[0];
        }
    }

    public static Player[] Players;

    void Awake()
    {
        Players = GameObject.FindObjectsOfType<Player>();
        //PlayerID = IDFactory.GetUniqueID();
    }

    //public void DrawCard()
    //{
    //    CardAsset drawnCard = deck.DrawCard();

    //    // if no drawn card, then use the discard pile.. 
    //    //if nothing in discard pile, you lose
    //    if (drawnCard == null)
    //    {

    //    }

        
    //}
}
