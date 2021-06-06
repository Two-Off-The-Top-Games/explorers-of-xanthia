using GameState.Events;
using System.Threading;
using TMPro;
using UnityEngine;

public class CombatEventPopupController : UIComponentWithQueueableActions
{
    public TextMeshProUGUI CombatEventPopupText;

    private void OnEnable()
    {
        CombatEvent.RegisterListener(OnCombatEvent);
    }

    private void OnDisable()
    {
        CombatEvent.DeregisterListener(OnCombatEvent);
    }

    private void OnCombatEvent(CombatEvent combatEvent)
    {
        EnqueueAction(() =>
        {
            CombatEventPopupText.text = combatEvent.CombatEventText;
        });
    }
}
