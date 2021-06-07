using GameState.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject CharacterPrefab;
    [Range(0.01f, 100f)]
    public float PercentOfAvailableVerticalScreenSpace;

    private void OnEnable()
    {
        SpawnCharactersEvent.RegisterListener(OnSpawnCharactersEvent);
    }

    private void OnSpawnCharactersEvent(SpawnCharactersEvent spawnCharactersEvent)
    {
        var rectTransform = GetComponent<RectTransform>();
        float spawnLocationYOffset = rectTransform.rect.height / (spawnCharactersEvent.NumberToSpawn + 1);
        float scaledSpawnLocationYOffest = spawnLocationYOffset * (PercentOfAvailableVerticalScreenSpace / 100f);
        float topOfContainer = rectTransform.rect.height / 2;
        float spawnLocationXCoordinate = rectTransform.rect.width / 4;
        for (int i = 0; i < spawnCharactersEvent.NumberToSpawn; i++)
        {
            float spawnLocationYCoordinate = topOfContainer - ((i + 1) * scaledSpawnLocationYOffest);
            var spawnedCharacter = Instantiate(CharacterPrefab, new Vector3(-spawnLocationXCoordinate, spawnLocationYCoordinate), Quaternion.identity, transform);
            var characterInstanceId = spawnedCharacter.GetInstanceID();
            new CharacterSpawnedEvent(characterInstanceId).Fire();
        }
    }
}
