using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolvePlayCommand : Command
{
    private Player player;
    private Player opponent;

    public ResolvePlayCommand(ref Player player, ref Player opponent)
    {
        this.player = player;
        this.opponent = opponent;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        Vector3 cardsEndLocation = Vector3.zero;
        Discard cardsEndDiscard = new Discard();
        bool shouldResolve = false;

        bool goToWar = player.CardInPlay.AutoWar || 
                       opponent.CardInPlay.AutoWar || 
                       player.CardInPlay.Value == opponent.CardInPlay.Value;

        if (goToWar)
        {
            // WAR
            (new ShowMessageCommand("  WAR!  ", GlobalSettings.MessageWarTime)).AddToQueue();

            // Play 3 down
            for (int i = 1; i < 4; i++)
            {
                (new DrawCardCommand(ref player)).AddToQueue();
                (new DrawCardCommand(ref opponent)).AddToQueue();

                //if (!player.HasLost && !opponent.HasLost) { break; }

                (new InstantiateCardCommand(ref player)).AddToQueue();
                (new InstantiateCardCommand(ref opponent)).AddToQueue();

                (new MoveCardsCommand(ref player,
                    new Vector3(player.inPlay.transform.position.x,
                                player.inPlay.transform.position.y - (0.4f * i),
                                player.inPlay.transform.position.z - (0.2f * i)),
                    ref opponent,
                    new Vector3(opponent.inPlay.transform.position.x,
                                opponent.inPlay.transform.position.y + (0.4f * i),
                                opponent.inPlay.transform.position.z - (0.2f * i)),
                    GlobalSettings.WarCardMoveTime)).AddToQueue();

                (new DelayCommand(GlobalSettings.WarStackDelayTime)).AddToQueue();
            }

            // Last card is the card that's actually played
            (new DrawCardCommand(ref player)).AddToQueue();
            (new DrawCardCommand(ref opponent)).AddToQueue();

            //if (!player.HasLost && !opponent.HasLost) { break; }

            (new InstantiateCardCommand(ref player)).AddToQueue();
            (new InstantiateCardCommand(ref opponent)).AddToQueue();


            (new MoveCardsCommand(ref player,
                    new Vector3(player.inPlay.transform.position.x,
                                player.inPlay.transform.position.y - (0.4f * 4),
                                player.inPlay.transform.position.z - (0.1f * 10)),
                    ref opponent,
                    new Vector3(opponent.inPlay.transform.position.x,
                                opponent.inPlay.transform.position.y + (0.4f * 4),
                                opponent.inPlay.transform.position.z - (0.1f * 10)),
                    GlobalSettings.CardMoveTime)).AddToQueue();

            //(new RevealCardsCommand(new GameObject[] { player.CardOnTable, opponent.CardOnTable })).AddToQueue();
            (new RevealCardsCommand(new Player[] { player, opponent })).AddToQueue();

            (new DelayCommand(GlobalSettings.RevealDelayTime)).AddToQueue();

            (new ResolvePlayCommand(ref player, ref opponent)).AddToQueue();
        }
        else if (player.CardInPlay.Value > opponent.CardInPlay.Value)
        {
            cardsEndLocation = player.discard.transform.position;
            cardsEndDiscard = player.discard;

            shouldResolve = true;
        }
        else if (opponent.CardInPlay.Value > player.CardInPlay.Value)
        {
            cardsEndLocation = opponent.discard.transform.position;
            cardsEndDiscard = opponent.discard;

            shouldResolve = true;
        }

        if (shouldResolve)
        {
            (new FaceDownCommand(GameManager.Instance.cardsOnTable.ToArray())).AddToQueue();

            (new MoveGameObjectCommand(GameManager.Instance.cardsOnTable.ToArray(), cardsEndLocation, GlobalSettings.CardMoveTime)).AddToQueue();
            (new ActionCommand(() =>
            {
                cardsEndDiscard.cards.AddRange(GameManager.Instance.cardsAtStake);
                cardsEndDiscard.discardedCards.AddRange(GameManager.Instance.cardsOnTable);
            })).AddToQueue();

            //(new FaceDownCommand(GameManager.Instance.cardsOnTable.ToArray())).AddToQueue();

            (new ActionCommand(() =>
            {
                GameManager.Instance.cardsAtStake.Clear();
                GameManager.Instance.cardsOnTable.Clear();

                foreach (var card in cardsEndDiscard.discardedCards)
                {
                    GameObject.Destroy(card);
                }
            })).AddToQueue();

            (new ChangeStateCommand(GameState.EndPhase)).AddToQueue();
        }

        Command.CommandExecutionComplete();
    }
}
