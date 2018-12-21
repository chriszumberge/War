using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDiscardToDeckCommand : Command
{
    private Player _player;

    public MoveDiscardToDeckCommand(ref Player player)
    {
        _player = player;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        _player.deck.cards.AddRange(_player.discard.cards);
        _player.discard.cards.Clear();

        // TODO animate
        foreach (var card in _player.discard.discardedCards)
        {
            GameObject.Destroy(card);
        }

        Command.CommandExecutionComplete();
    }
}
