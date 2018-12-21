using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDownCommand : Command {

    GameObject[] _cards;

    public FaceDownCommand(GameObject[] cards)
    {
        _cards = cards;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        MovementManager.Instance.Rotate(_cards, new Quaternion(0, 1.0f, 0, 0), GlobalSettings.FlipDownTime);
    }
}
