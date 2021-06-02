using GameState.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatEventLogUpdater : UIComponentWithQueueableActions
{
    public TextMeshProUGUI CombatEventLogText;
    public ScrollRect CombatEventLogScrollRect;
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
            float currentScrollPosition = CombatEventLogScrollRect.verticalNormalizedPosition;

            CombatEventLogText.text += $"{combatEvent.CombatEventText}\n";
            Canvas.ForceUpdateCanvases();
            if (!(currentScrollPosition > 0f))
            {
                CombatEventLogScrollRect.verticalNormalizedPosition = 0;
            }
        });
    }
}
