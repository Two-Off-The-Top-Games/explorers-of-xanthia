using GameState.Events;
using System.Collections.Generic;

public class ActiveCharacters
{
    public IList<int> ActiveCharacterIds
    {
        get
        {
            return _activeCharacterIds;
        }
    }
    private List<int> _activeCharacterIds = new List<int>();

    public ActiveCharacters()
    {
        CharacterSpawnedEvent.RegisterListener(OnCharacterSpawnedEvent);
    }

    private void OnCharacterSpawnedEvent(CharacterSpawnedEvent characterSpawnedEvent)
    {
        _activeCharacterIds.Add(characterSpawnedEvent.CharacterInstanceId);
    }
}
