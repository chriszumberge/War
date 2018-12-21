using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCardsCommand : Command {

    Player _player1;
    Vector3 _position1;
    Player _player2;
    Vector3 _position2;
    float _duration;

    public MoveCardsCommand(ref Player player1, Vector3 position1, float duration)
    {
        _player1 = player1;
        _position1 = position1;
        _duration = duration;
    }

    public MoveCardsCommand(ref Player player1, Vector3 position1, ref Player player2, Vector3 position2, float duration)
    {
        _player1 = player1;
        _position1 = position1;
        _player2 = player2;
        _position2 = position2;
        _duration = duration;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        if (_player2 == null || _position2 == null)
        {
            MovementManager.Instance.Move(
                new GameObject[] { _player1.CardOnTable },
                new Vector3[] { _position1 },
                _duration);
        }
        else
        {
            MovementManager.Instance.Move(
                new GameObject[] { _player1.CardOnTable, _player2.CardOnTable },
                new Vector3[] { _position1, _position2 },
                _duration);
        }
    }
}
