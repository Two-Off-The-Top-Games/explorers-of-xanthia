using GameState.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    private void OnEnable()
    {
        StartGameEvent.RegisterListener(OnStartGameEvent);
    }

    private void OnStartGameEvent(StartGameEvent _)
    {
        gameObject.SetActive(false);
    }

}
