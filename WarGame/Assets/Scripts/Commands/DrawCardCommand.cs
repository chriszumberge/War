using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardCommand : Command {

    Player _player;
    public DrawCardCommand(ref Player player)
    {
        _player = player;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        Deck deck = _player.deck;

        if (deck.cards.Count == 0)
        {
            if (_player.discard.cards.Count == 0)
            {
                _player.HasLost = true;
                Command.CommandQueue.Clear();
                (new ChangeStateCommand(GameState.GameOverPhase)).AddToQueue();
            }
            else
            {
                // TODO animate
                (new DrawCardCommand(ref _player)).InsertAtFront();
                (new MoveDiscardToDeckCommand(ref _player)).InsertAtFront();
            }
        }

        if (!_player.HasLost && deck.cards.Count > 0)
        {
            CardAsset drawnCard = deck.DrawCard();
            if (drawnCard != null)
            {
                _player.CardInPlay = drawnCard;
                GameManager.Instance.cardsAtStake.Add(_player.CardInPlay);
            }
        }

        Command.CommandExecutionComplete();
    }
}
