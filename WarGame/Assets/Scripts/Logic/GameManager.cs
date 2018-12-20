using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public List<CardAsset> AvailableCards = new List<CardAsset>();

    public Player player;
    public Player opponent;

    // GAME MAKER STATES.. ON UPDATE DEPENDING ON STATE.. maybe get rid of command queue? or cmd queue in executing state
    public GameState CurrentState = GameState.GameBeginPhase;

    public GameObject CardPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        AvailableCards.Shuffle();

        for (int i = 0; i < AvailableCards.Count; i++)
        {
            if (i % 2 == 0)
            {
                player.deck.cards.Add(AvailableCards[i]);
            }
            else
            {
                opponent.deck.cards.Add(AvailableCards[i]);
            }
        }

        player.deck.Init();
        opponent.deck.Init();
        //player.deck.cards.Shuffle();
        //opponent.deck.cards.Shuffle();

        //MessageManager.Instance.ShowMessage("Game Begin", 3f);
    }

    public static float TimeInStage = 0;
    //float _timeInStage = 0;

    void Update()
    {
        // If we're not currently playing out a command..
        if (!Command.playingQueue)
        {
            // If there's no commands in the queue, figure out what to do next??
            if (Command.CommandQueue.Count == 0)
            {
                if (CurrentState == GameState.GameBeginPhase)
                {
                    var gameBeginMsgCmd = new ShowMessageCommand("Game Begin", 3f);
                    gameBeginMsgCmd.AddToQueue();

                    var changeStateCmd = new ChangeStateCommand(GameState.DrawPhase);
                    changeStateCmd.AddToQueue();
                }
                else if (CurrentState == GameState.DrawPhase)
                {
                    TimeInStage += Time.deltaTime;

                    if (TimeInStage > 5)
                    {
                        // Show a Tip to tap the deck
                    }
                }
                else if (CurrentState == GameState.PlayPhase)
                {

                }
                else if (CurrentState == GameState.WarPhase)
                {

                }
                else if (CurrentState == GameState.ResolvePhase)
                {

                }
                else if (CurrentState == GameState.EndPhase)
                {
                    bool gameIsOver = false;

                    // TODO move to "HandleEmptyDeck" method that way it's also called if a player is drawing for war?
                    if (player.deck.cards.Count == 0)
                    {
                        if (player.discard.cards.Count == 0)
                        {
                            (new ShowMessageCommand("You Lose!", 5.0f)).AddToQueue();
                            (new ChangeStateCommand(GameState.GameOverPhase)).AddToQueue();
                            gameIsOver = true;
                        }
                        else
                        {
                            // TODO animate
                            player.deck.cards.AddRange(player.discard.cards);
                            player.discard.cards.Clear();
                        }
                    }
                    
                    if (opponent.deck.cards.Count == 0)
                    {
                        if (opponent.discard.cards.Count == 0)
                        {
                            (new ShowMessageCommand("You Win!", 5.0f)).AddToQueue();
                            (new ChangeStateCommand(GameState.GameOverPhase)).AddToQueue();
                            gameIsOver = true;
                        }
                        else
                        {
                            // TODO animate
                            opponent.deck.cards.AddRange(opponent.discard.cards);
                            opponent.discard.cards.Clear();
                        }
                    }

                    if (!gameIsOver)
                    {
                        (new ChangeStateCommand(GameState.DrawPhase)).AddToQueue();
                    }
                }
                else if (CurrentState == GameState.GameOverPhase)
                {

                }
            }


            //if (Command.CommandQueue.Count > 0 && !Command.playingQueue)
            if (Command.CommandQueue.Count > 0)
            {
                Command cmd = Command.CommandQueue.Dequeue();
                cmd.StartCommandExecution();
            }
        }
    }

    public void PlayerDrawRequested(Deck drawDeck)
    {
        if (CurrentState == GameState.DrawPhase)
        {
            CardAsset card = drawDeck.DrawCard();
            player.CardInPlay = card;
            
            GameObject drawnCard = Instantiate(CardPrefab,
                                               new Vector3(drawDeck.transform.position.x,
                                                           drawDeck.transform.position.y,
                                                           -1),
                                               new Quaternion(0, 180, 0, 0)
                                               );
            //drawDeck.transform.rotation);
            //OneCardManager cardMgr = drawnCard.GetComponent<OneCardManager>();
            //cardMgr.cardAsset = card;
            //cardMgr.redraw();
            (drawnCard.GetComponent<OneCardManager>()).cardAsset = card;
            player.CardOnTable = drawnCard;

            CardAsset oppCard = opponent.deck.DrawCard();
            opponent.CardInPlay = oppCard;
            GameObject oppDrawnCard = GameObject.Instantiate(CardPrefab,
                                                               new Vector3(opponent.deck.transform.position.x,
                                                                           opponent.deck.transform.position.y,
                                                                           -1),
                                                               new Quaternion(0, 180, 0, 0));
            //OneCardManager oppCardMgr = oppDrawnCard.GetComponent<OneCardManager>();
            //oppCardMgr.cardAsset = oppCard;
            //oppCardMgr.redraw();
            (oppDrawnCard.GetComponent<OneCardManager>()).cardAsset = oppCard;
            opponent.CardOnTable = oppDrawnCard;

            if (card != null)
            {
                (new ChangeStateCommand(GameState.DrawTransitionPhase)).AddToQueue();

                //(new MoveGameObjectCommand(drawnCard, player.inPlay.transform.position, 0.5f)).AddToQueue();
                //(new MoveGameObjectCommand(oppDrawnCard, opponent.inPlay.transform.position, 0.5f)).AddToQueue();
                (new MoveGameObjectCommand(new GameObject[] { drawnCard, oppDrawnCard },
                    new Vector3[] { player.inPlay.transform.position, opponent.inPlay.transform.position },
                    0.5f)).AddToQueue();
                
                (new ChangeStateCommand(GameState.PlayPhase)).AddToQueue();

                (new RevealCardsCommand(drawnCard, oppDrawnCard)).AddToQueue();

                (new DelayCommand(1.0f)).AddToQueue();

                (new ChangeStateCommand(GameState.ResolvePhase)).AddToQueue();

                (new ResolvePlayCommand(player, opponent)).AddToQueue();
            }
            else
            {
                (new ChangeStateCommand(GameState.GameOverPhase)).AddToQueue();
            }
        }
    }

    public void Delay(float duration)
    {
        StartCoroutine(DelayCoroutine(duration));
    }

    IEnumerator DelayCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        Command.CommandExecutionComplete();
    }
}
