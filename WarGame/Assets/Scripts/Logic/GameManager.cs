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

    public GameObject GameMenu;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        BeginGame();
    }

    void BeginGame()
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

        CurrentState = GameState.GameBeginPhase;
    }

    void ClearGame()
    {
        GameMenu.SetActive(false);

        Command.CommandQueue.Clear();

        cardsOnTable.Clear();
        cardsAtStake.Clear();

        // TODO group into Player.Clear/Reset method
        player.deck.cards.Clear();
        player.discard.cards.Clear();
        player.HasLost = false;

        opponent.deck.cards.Clear();
        opponent.discard.cards.Clear();
        opponent.HasLost = false;
    }

    public void ResetGame()
    {
        ClearGame();
        BeginGame();
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
                    var gameBeginMsgCmd = new ShowMessageCommand("Game Begin", GlobalSettings.MessageStartTime);
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
                else if (CurrentState == GameState.DrawTransitionPhase)
                {
                    //(new MoveGameObjectCommand(drawnCard, player.inPlay.transform.position, 0.5f)).AddToQueue();
                    //(new MoveGameObjectCommand(oppDrawnCard, opponent.inPlay.transform.position, 0.5f)).AddToQueue();
                    (new MoveGameObjectCommand(new GameObject[] { player.CardOnTable, opponent.CardOnTable },
                        new Vector3[] { player.inPlay.transform.position, opponent.inPlay.transform.position },
                        GlobalSettings.CardMoveTime)).AddToQueue();

                    (new ChangeStateCommand(GameState.PlayPhase)).AddToQueue();
                }
                else if (CurrentState == GameState.PlayPhase)
                {
                    (new RevealCardsCommand(new GameObject[] { player.CardOnTable, opponent.CardOnTable })).AddToQueue();

                    (new DelayCommand(GlobalSettings.RevealDelayTime)).AddToQueue();

                    (new ChangeStateCommand(GameState.ResolvePhase)).AddToQueue();

                    (new ResolvePlayCommand(ref player, ref opponent)).AddToQueue();
                }
                else if (CurrentState == GameState.WarPhase)
                {

                }
                else if (CurrentState == GameState.ResolvePhase)
                {
                }
                else if (CurrentState == GameState.EndPhase)
                {
                    if (player.deck.cards.Count == 0)
                    {
                        if (player.discard.cards.Count == 0)
                        {
                            player.HasLost = true;
                        }
                        else
                        {
                            (new MoveDiscardToDeckCommand(ref player)).AddToQueue();
                        }
                    }
                    if (opponent.deck.cards.Count == 0)
                    {
                        if (opponent.discard.cards.Count == 0)
                        {
                            opponent.HasLost = true;
                        }
                        else
                        {
                            (new MoveDiscardToDeckCommand(ref opponent)).AddToQueue();
                        }
                    }

                    if (player.HasLost || opponent.HasLost)
                    {
                        (new ChangeStateCommand(GameState.GameOverPhase)).AddToQueue();
                    }
                    else
                    {
                        (new ChangeStateCommand(GameState.DrawPhase)).AddToQueue();
                    }
                }
                else if (CurrentState == GameState.GameOverPhase)
                {
                    Command.CommandQueue.Clear();

                    if (player.HasLost)
                    {
                        (new ShowMessageCommand("You Lose!", GlobalSettings.MessageGameEndTime)).AddToQueue();
                    }
                    else
                    {
                        (new ShowMessageCommand("You Win!", GlobalSettings.MessageGameEndTime)).AddToQueue();
                    }

                    (new ChangeStateCommand(GameState.NotPlaying)).AddToQueue();
                }
                else if (CurrentState == GameState.NotPlaying)
                {
                    if (!GameMenu.activeSelf)
                    {
                        GameMenu.SetActive(true);
                    }
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

    public List<CardAsset> cardsAtStake = new List<CardAsset>();
    public List<GameObject> cardsOnTable = new List<GameObject>();

    public void PlayerDrawRequested()
    {
        if (CurrentState == GameState.DrawPhase)
        {

            (new DrawCardCommand(ref player)).AddToQueue();
            (new DrawCardCommand(ref opponent)).AddToQueue();

            if (!player.HasLost && !opponent.HasLost)
            {
                (new InstantiateCardCommand(ref player)).AddToQueue();
                (new InstantiateCardCommand(ref opponent)).AddToQueue();

                (new ChangeStateCommand(GameState.DrawTransitionPhase)).AddToQueue();
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
