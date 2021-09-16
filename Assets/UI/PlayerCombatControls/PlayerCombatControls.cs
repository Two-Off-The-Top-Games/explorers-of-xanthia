using Entities.Events;
using Events.Inventory;
using GameState.Events;
using UnityEngine;

public class PlayerCombatControls : UIComponentWithQueueableActions
{
    public GameObject AttackButton;
    public GameObject EndTurnButton;
    public GameObject CancelItemButton;

    private void OnEnable()
    {
        SetActiveForAllControls(false);
        CharacterSpawnedEvent.RegisterListener(OnCharacterSpawnedEvent);
        UseItemStartedEvent.RegisterListener(OnUseItemStartedEvent);
        UseItemEndedEvent.RegisterListener(OnUseItemEndedEvent);
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        CharacterTurnStartedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnStartedEvent);
        CharacterTurnEndedEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterTurnEndedEvent);
        CharacterStartedAttackEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterStartedAttackEvent);
        CharacterFinishedAttackEvent.RegisterListener(characterSpawnedEvent.CharacterInstanceId, OnCharacterFinishedAttackEvent);
    }

    private void SetActiveForAllControls(bool activeState)
    {
        SetActiveForCombatControls(activeState);
        CancelItemButton.SetActive(activeState);
    }

    private void SetActiveForCombatControls(bool activeState)
    {
        AttackButton.SetActive(activeState);
        EndTurnButton.SetActive(activeState);
    }

    private void OnCharacterTurnStartedEvent(CharacterTurnStartedEvent _)
    {
        EnqueueAction(() => SetActiveForCombatControls(true));
    }

    private void OnCharacterTurnEndedEvent(CharacterTurnEndedEvent _)
    {
        EnqueueAction(() => SetActiveForCombatControls(false));
    }

    private void OnCharacterStartedAttackEvent(CharacterStartedAttackEvent _)
    {
        EnqueueAction(() => SetActiveForCombatControls(false));
    }

    private void OnCharacterFinishedAttackEvent(CharacterFinishedAttackEvent _)
    {
        EnqueueAction(() => SetActiveForCombatControls(true));
    }

    private void OnUseItemStartedEvent(UseItemStartedEvent _)
    {
        EnqueueAction(() =>
        {
            SetActiveForCombatControls(false);
            CancelItemButton.SetActive(true);
        });
    }

    private void OnUseItemEndedEvent(UseItemEndedEvent _)
    {
        EnqueueAction(() =>
        {
            SetActiveForCombatControls(true);
            CancelItemButton.SetActive(false);
        });
    }
}
