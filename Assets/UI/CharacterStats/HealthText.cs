using Entities.Events;

public class HealthText : TextWithQueueableUpdate
{
    private void OnEnable()
    {
        CharacterHealthChangedEvent.RegisterListener(UpdateHealthText);
    }

    private void OnDisable()
    {
        CharacterHealthChangedEvent.DeregisterListener(UpdateHealthText);
    }

    private void UpdateHealthText(CharacterHealthChangedEvent characterHealthChangedEvent)
    {
        EnqueueUpdateTextAction(new UpdateTextAction($"HP: {characterHealthChangedEvent.CurrentHealth}/{characterHealthChangedEvent.MaxHealth}"));
    }
}
