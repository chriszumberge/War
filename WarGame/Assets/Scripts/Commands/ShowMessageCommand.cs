using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessageCommand : Command {

    public ShowMessageCommand(string message, float duration)
    {
        _message = message;
        _duration = duration;
    }

    string _message;
    float _duration;
    
    public override void StartCommandExecution()
    {
        MessageManager.Instance.ShowMessage(_message, _duration);
    }
}
