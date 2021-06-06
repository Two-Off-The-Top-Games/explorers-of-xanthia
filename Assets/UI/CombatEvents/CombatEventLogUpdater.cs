using GameState.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatEventLogUpdater : UIComponentWithQueueableActions
{
    public TextMeshProUGUI CombatEventLogText;
    public ScrollRect CombatEventLogScrollRect;
    private static readonly object _locker = new object();
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
            lock(_locker)
            {
                float currentScrollPosition = CombatEventLogScrollRect.verticalNormalizedPosition;

                CombatEventLogText.text += $"{combatEvent.CombatEventText}\n";
                Canvas.ForceUpdateCanvases();
                if (currentScrollPosition <= 0.01f)
                {
                    CombatEventLogScrollRect.verticalNormalizedPosition = 0;
                }
            }
        });
    }
}
