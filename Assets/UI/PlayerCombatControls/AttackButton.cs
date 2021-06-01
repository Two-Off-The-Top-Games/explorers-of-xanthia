using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : UIComponentWithQueueableActions
{
    private Button _button;

    private void OnEnable()
    {
        CharacterActionPointsChangedEvent.RegisterListener(OnCharacterActionPointsChangedEvent);
        _button = gameObject.GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CharacterAttackEvent().Fire();
    }

    private void OnCharacterActionPointsChangedEvent(CharacterActionPointsChangedEvent characterActionPointsChangedEvent)
    {
        EnqueueAction(() => _button.interactable = characterActionPointsChangedEvent.CurrentActionPoints > 0);
        
    }
}
