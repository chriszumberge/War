using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObjectCommand : Command {
    private GameObject[] cards;
    private Vector3[] positions;
    private float duration;

    public MoveGameObjectCommand(GameObject[] cards, Vector3[] positions, float duration)
    {
        this.cards = cards;
        this.positions = positions;
        this.duration = duration;
    }

    public MoveGameObjectCommand(GameObject[] cards, Vector3 position, float duration)
    {
        this.cards = cards;
        this.duration = duration;

        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < cards.Length; i++)
        {
            positions.Add(new Vector3(position.x, position.y, position.z));
        }
        this.positions = positions.ToArray();
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        MovementManager.Instance.Move(cards, positions, duration);
    }
}
