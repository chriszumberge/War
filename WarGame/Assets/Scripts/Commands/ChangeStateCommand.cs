using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateCommand : Command
{
    public ChangeStateCommand(GameState newGameState)
    {
        _newGameState = newGameState;
    }

    GameState _newGameState;

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        GameManager.Instance.CurrentState = _newGameState;

        GameManager.TimeInStage = 0;

        Command.CommandExecutionComplete();
    }
}
