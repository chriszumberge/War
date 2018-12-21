using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCardCommand : Command {

    Player _player;
    public InstantiateCardCommand(ref Player player)
    {
        _player = player;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        GameObject cardObject = GameObject.Instantiate(GameManager.Instance.CardPrefab,
            new Vector3(_player.deck.transform.position.x,
                        _player.deck.transform.position.y,
                        -1),
            new Quaternion(0, 1, 0, 0));

        (cardObject.GetComponent<OneCardManager>()).cardAsset = _player.CardInPlay;
        _player.CardOnTable = cardObject;

        GameManager.Instance.cardsOnTable.Add(_player.CardOnTable);

        _player.CardOnTable.SetActive(true);

        Command.CommandExecutionComplete();
    }
}
