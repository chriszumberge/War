using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour {

    public List<CardAsset> cards = new List<CardAsset>();

    public List<GameObject> discardedCards = new List<GameObject>();

    public GameObject deckRepresentation;
    private int _allCardCount;

    private float _maxY = -1.7f;
    private float _minY = 0.5f;
    private float _cardSize;

    private void Awake()
    {
        _allCardCount = GameManager.Instance.AvailableCards.Count;

        _cardSize = (_maxY - _minY) / _allCardCount / 2;
    }

    private void Update()
    {
        if(cards.Count > 0)
        {
            deckRepresentation.SetActive(true);

            deckRepresentation.transform.position = new Vector3(deckRepresentation.transform.position.x,
                                                                deckRepresentation.transform.position.y,
                                                                _minY + _cardSize * cards.Count);
        }
        else
        {
            deckRepresentation.SetActive(false);
        }
    }
}
