using Entities.Events;

public class APText : TextWithQueueableUpdate
{

    private void OnEnable()
    {
        CharacterActionPointsChangedEvent.RegisterListener(UpdateAPText);
    }

    private void OnDisable()
    {
        CharacterActionPointsChangedEvent.DeregisterListener(UpdateAPText);
    }

    private void UpdateAPText(CharacterActionPointsChangedEvent characterActionPointsChangedEvent)
    {
        EnqueueUpdateTextAction(new UpdateTextAction($"AP: {characterActionPointsChangedEvent.CurrentActionPoints}/{characterActionPointsChangedEvent.MaxActionPoints}"));
    }
}
