using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIComponentWithQueueableActions : MonoBehaviour
{
    private Queue<Action> _actions = new Queue<Action>();

    protected void Update()
    {
        if (_actions.Count > 0)
        {
            var action = _actions.Dequeue();
            action();
        }
    }

    protected void EnqueueAction(Action action)
    {
        _actions.Enqueue(action);
    }
}
