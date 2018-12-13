using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameManager Instance;

    public List<CardAsset> AvailableCards = new List<CardAsset>();

    public Player player;
    public Player opponent;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
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
        var gameBeginMsgCmd = new ShowMessageCommand("Game Begin", 3f);
        gameBeginMsgCmd.AddToQueue();
	}

    void Update()
    {
        if (Command.CommandQueue.Count > 0)
        {
            Command cmd = Command.CommandQueue.Dequeue();
            cmd.StartCommandExecution();
        }
    }
}
