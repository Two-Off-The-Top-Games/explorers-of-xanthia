using Entities.Events;

public class LevelText : TextWithQueueableUpdate
{
    private void OnEnable()
    {
        CharacterLevelChangedEvent.RegisterListener(UpdateLevelText);
    }

    private void OnDisable()
    {
        CharacterLevelChangedEvent.DeregisterListener(UpdateLevelText);
    }

    private void UpdateLevelText(CharacterLevelChangedEvent characterLevelChangedEvent)
    {
        EnqueueUpdateTextAction(new UpdateTextAction($"Level: {characterLevelChangedEvent.Level}"));
    }
}
