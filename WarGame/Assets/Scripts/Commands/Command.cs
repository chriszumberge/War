using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Command
{
    public static Queue<Command> CommandQueue = new Queue<Command>();
    public static bool playingQueue = false;

    public virtual void AddToQueue()
    {
        CommandQueue.Enqueue(this);
    }

    public virtual void InsertAtFront()
    {
        Queue<Command> tempQueue = new Queue<Command>();
        tempQueue.Enqueue(this);
        while (CommandQueue.Count > 0)
        {
            tempQueue.Enqueue(CommandQueue.Dequeue());
        }
        while (tempQueue.Count > 0)
        {
            CommandQueue.Enqueue(tempQueue.Dequeue());
        }
    }

    public virtual void StartCommandExecution()
    {
        playingQueue = true;
        // list of everything that we have to do with this command (draw a card, play a card, play spell effect, etc...)
        // there are 2 options of timing : 
        // 1) use tween sequences and call CommandExecutionComplete in OnComplete()
        // 2) use coroutines (IEnumerator) and WaitFor... to introduce delays, call CommandExecutionComplete() in the end of coroutine
    }

    public static void CommandExecutionComplete()
    {
        playingQueue = false;
    }
}
