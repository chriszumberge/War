using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealCardsCommand : Command
{
    private GameObject playerCard;
    private GameObject oppDrawnCard;

    public RevealCardsCommand(GameObject playerCard, GameObject oppDrawnCard)
    {
        this.playerCard = playerCard;
        this.oppDrawnCard = oppDrawnCard;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();
        
        MovementManager.Instance.Rotate(new GameObject[] { playerCard, oppDrawnCard }, new Quaternion(0, 0, 0, 1.0f), 0.5f);
    }
}
