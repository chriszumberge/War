using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Deck : MonoBehaviour {

    public List<CardAsset> cards = new List<CardAsset>();

    public GameObject deckRepresentation;

    // What was this for?
    //[SerializeField] private bool inPlay = false;

    private int _fullDeckCardCount;

    private float _maxY = -0.85f;
    private float _minY = 0f;
    private float _cardSize;

    void Awake()
    {
        // Cards are being dealt out by GameManager 
        //cards.Shuffle();
    }

    void Update()
    {
        if (cards.Count > 0)
        {
            deckRepresentation.SetActive(true);

            deckRepresentation.transform.position = new Vector3(deckRepresentation.transform.position.x,
                                                                deckRepresentation.transform.position.y,
                                                                _cardSize * cards.Count);
        }
        else
        {
            deckRepresentation.SetActive(false);
        }
    }

    public void Init()
    {
        Debug.Log("Initing");

        cards.Shuffle();

        _fullDeckCardCount = cards.Count;

        _cardSize = (_maxY - _minY) / _fullDeckCardCount;
    }

    /// <summary>
    /// Draws a card from the deck, removing it from the deck, and returns it.
    /// </summary>
    /// <returns>The card, or null, if the deck is empty.</returns>
    public CardAsset DrawCard()
    {
        CardAsset card = cards.FirstOrDefault();

        if (card != null)
        {
            cards.Remove(card);
        }

        return card;
    }
}
