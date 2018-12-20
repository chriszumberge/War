using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : Command {

    System.Action _action;
    public ActionCommand(System.Action action)
    {
        _action = action;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        _action.Invoke();

        Command.CommandExecutionComplete();
    }
}
