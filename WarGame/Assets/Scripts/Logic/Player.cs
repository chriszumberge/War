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

    public virtual void OnTurnStart()
    {
        // add one mana crystal to the pool;
        Debug.Log("In ONTURNSTART for "+ gameObject.name);
    }

    public void DrawACard(bool fast = false)
    {
        if (deck.cards.Count > 0)
        {
            //if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
            //{
            //    // 1) save index to place a visual card into visual hand
            //    int indexToPlaceACard = hand.CardsInHand.Count;
            //    // 2) logic: add card to hand
            //    CardLogic newCard = new CardLogic(deck.cards[0]);
            //    newCard.owner = this;
            //    hand.CardsInHand.Add(newCard);
            //    // Debug.Log(hand.CardsInHand.Count);
            //    // 3) logic: remove the card from the deck
            //    deck.cards.RemoveAt(0);
            //    // 4) create a command
            //    new DrawACardCommand(hand.CardsInHand[indexToPlaceACard], this, indexToPlaceACard, fast, fromDeck: true).AddToQueue(); 
            //}
        }
        else
        {
            // there are no cards in the deck, take fatigue damage.
        }
       
    }
}
