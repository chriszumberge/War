using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolvePlayCommand : Command
{
    private Player player;
    private Player opponent;

    CardAsset playerCard;
    CardAsset opponentCard;

    public ResolvePlayCommand(Player player, Player opponent)
    {
        this.player = player;
        playerCard = player.CardInPlay;
        this.opponent = opponent;
        opponentCard = opponent.CardInPlay;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        bool goToWar = playerCard.name.Equals("Jester") || opponentCard.name.Equals("Jester") || playerCard.Value == opponentCard.Value;

        if (goToWar)
        {
            // WAR
            (new ShowMessageCommand("  WAR!  ", 1.5f)).AddToQueue();

            List<CardAsset> cardsAtStake = new List<CardAsset>() { player.CardInPlay, opponent.CardInPlay };
            List<GameObject> cardsOnTable = new List<GameObject>() { player.CardOnTable, opponent.CardOnTable };

            GameObject originalPlayerCard = player.CardOnTable;
            GameObject originalOpponentCard = opponent.CardOnTable;

            // Play 3 down
            for (int i = 1; i < 4; i++)
            {
                CardAsset playerCard = player.deck.DrawCard();
                cardsAtStake.Add(playerCard);
                CardAsset opponentCard = opponent.deck.DrawCard();
                cardsAtStake.Add(opponentCard);

                GameObject playerDrawnCard = GameObject.Instantiate(GameManager.Instance.CardPrefab,
                    new Vector3(player.deck.transform.position.x,
                                player.deck.transform.position.y,
                                -1 - (0.2f * i)),
                    new Quaternion(0, 1, 0, 0));
                (playerDrawnCard.GetComponent<OneCardManager>()).cardAsset = playerCard;

                GameObject opponentDrawnCard = GameObject.Instantiate(GameManager.Instance.CardPrefab,
                    new Vector3(opponent.deck.transform.position.x,
                                opponent.deck.transform.position.y,
                                -1 - (0.2f * i)),
                    new Quaternion(0, 1, 0, 0));
                (opponentDrawnCard.GetComponent<OneCardManager>()).cardAsset = opponentCard;

                cardsOnTable.Add(playerDrawnCard);
                cardsOnTable.Add(opponentDrawnCard);

                (new MoveGameObjectCommand(
                    new GameObject[] { playerDrawnCard, opponentDrawnCard },
                    new Vector3[] {
                        new Vector3(player.inPlay.transform.position.x,
                                    player.inPlay.transform.position.y - (0.4f * i),
                                    player.inPlay.transform.position.z - (0.2f * i)),
                        new Vector3(opponent.inPlay.transform.position.x,
                                    opponent.inPlay.transform.position.y + (0.4f * i),
                                    opponent.inPlay.transform.position.z - (0.2f * i))
                    },
                    0.5f)).AddToQueue();

                (new DelayCommand(0.25f)).AddToQueue();
            }

            // Last card is the card that's actually played
            CardAsset finalPlayerCard = player.deck.DrawCard();
            cardsAtStake.Add(finalPlayerCard);
            CardAsset finalOpponentCard = opponent.deck.DrawCard();
            cardsAtStake.Add(finalOpponentCard);

            GameObject finalPlayerDrawnCard = GameObject.Instantiate(GameManager.Instance.CardPrefab,
                new Vector3(player.deck.transform.position.x,
                            player.deck.transform.position.y,
                            -1 - (0.1f * 10)),
                new Quaternion(0, 1, 0, 0));
            (finalPlayerDrawnCard.GetComponent<OneCardManager>()).cardAsset = finalPlayerCard;

            GameObject finalOpponentDrawnCard = GameObject.Instantiate(GameManager.Instance.CardPrefab,
                new Vector3(opponent.deck.transform.position.x,
                            opponent.deck.transform.position.y,
                            -1 - (0.1f * 10)),
                new Quaternion(0, 1, 0, 0));
            (finalOpponentDrawnCard.GetComponent<OneCardManager>()).cardAsset = finalOpponentCard;

            cardsOnTable.Add(finalPlayerDrawnCard);
            cardsOnTable.Add(finalOpponentDrawnCard);

            (new MoveGameObjectCommand(
                new GameObject[] { finalPlayerDrawnCard, finalOpponentDrawnCard },
                new Vector3[] {
                        new Vector3(player.inPlay.transform.position.x,
                                    player.inPlay.transform.position.y - (0.4f * 4),
                                    player.inPlay.transform.position.z - (0.1f * 10)),
                        new Vector3(opponent.inPlay.transform.position.x,
                                    opponent.inPlay.transform.position.y + (0.4f * 4),
                                    opponent.inPlay.transform.position.z - (0.1f * 10))
                },
                0.5f)).AddToQueue();

            (new RevealCardsCommand(finalPlayerDrawnCard, finalOpponentDrawnCard)).AddToQueue();

            (new DelayCommand(1.0f)).AddToQueue();

            // TODO handle a second war!.. probably make this whole command recursive..
            if (finalPlayerCard.Value > finalOpponentCard.Value)
            {
                (new ShowMessageCommand("You Win the War!", 1.5f)).AddToQueue();
                (new MoveGameObjectCommand(cardsOnTable.ToArray(),
                    player.discard.transform.position, 0.5f)).AddToQueue();
                (new ActionCommand(() =>
                {
                    player.discard.cards.AddRange(cardsAtStake);
                })).AddToQueue();
            }
            else if (finalOpponentCard.Value > finalPlayerCard.Value)
            {
                (new ShowMessageCommand("Opponent Wins the War", 1.5f)).AddToQueue();
                (new MoveGameObjectCommand(cardsOnTable.ToArray(),
                    opponent.discard.transform.position, 0.5f)).AddToQueue();
                (new ActionCommand(() =>
                {
                    opponent.discard.cards.AddRange(cardsAtStake);
                })).AddToQueue();
            }

            (new FaceDownCommand(finalPlayerDrawnCard, finalOpponentDrawnCard)).AddToQueue();
            (new FaceDownCommand(originalPlayerCard, originalOpponentCard)).AddToQueue();
        }
        else if (playerCard.Value > opponentCard.Value)
        {
            (new MoveGameObjectCommand(new GameObject[] { player.CardOnTable, opponent.CardOnTable },
                new Vector3[] { player.discard.transform.position, player.discard.transform.position }, 0.5f)).AddToQueue();

            (new ActionCommand(() => { player.discard.cards.Add(opponent.CardInPlay); player.discard.cards.Add(player.CardInPlay); })).AddToQueue();
        }
        else if (opponentCard.Value > playerCard.Value)
        {
            (new MoveGameObjectCommand(new GameObject[] { player.CardOnTable, opponent.CardOnTable },
                new Vector3[] { opponent.discard.transform.position, opponent.discard.transform.position }, 0.5f)).AddToQueue();

            (new ActionCommand(() => { opponent.discard.cards.Add(opponent.CardInPlay); opponent.discard.cards.Add(player.CardInPlay); })).AddToQueue();
        }

        (new FaceDownCommand(player.CardOnTable, opponent.CardOnTable)).AddToQueue();

        (new ChangeStateCommand(GameState.EndPhase)).AddToQueue();

        Command.CommandExecutionComplete();
    }
}
