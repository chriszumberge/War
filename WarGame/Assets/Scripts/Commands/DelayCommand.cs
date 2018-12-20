using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayCommand : Command {

    float duration;
    public DelayCommand(float duration)
    {
        this.duration = duration;
    }

    public override void StartCommandExecution()
    {
        base.StartCommandExecution();

        GameManager.Instance.Delay(duration);
    }
}
