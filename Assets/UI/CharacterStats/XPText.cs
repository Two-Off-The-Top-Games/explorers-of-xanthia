using Entities.Events;

public class XPText : TextWithQueueableUpdate
{

    private void OnEnable()
    {
        CharacterXPChangedEvent.RegisterListener(UpdateXPText);
    }

    private void OnDisable()
    {
        CharacterXPChangedEvent.DeregisterListener(UpdateXPText);
    }

    private void UpdateXPText(CharacterXPChangedEvent characterXPChangedEvent)
    {
        EnqueueUpdateTextAction(new UpdateTextAction($"XP: {characterXPChangedEvent.XP}"));
    }
}
