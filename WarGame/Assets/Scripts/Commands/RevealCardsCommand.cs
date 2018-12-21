using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealCardsCommand : Command
{
    GameObject[] _cards;
    Player[] _players;

    public RevealCardsCommand(GameObject[] cards)
    {
        _cards = cards;
    }

    public RevealCardsCommand(Player[] players)
    {
        _players = players;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();
        
        if (_cards == default(GameObject[]))
        {
            List<GameObject> cards = new List<GameObject>();
            foreach(var player in _players)
            {
                cards.Add(player.CardOnTable);
            }
            _cards = cards.ToArray();
        }

        MovementManager.Instance.Rotate(_cards, new Quaternion(0, 0, 0, 1.0f), GlobalSettings.FlipUpTime);
    }
}
