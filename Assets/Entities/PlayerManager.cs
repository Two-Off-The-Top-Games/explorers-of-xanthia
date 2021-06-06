using GameState.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    [Range(0.01f, 100f)]
    public float PercentOfAvailableVerticalScreenSpace;

    private void OnEnable()
    {
        SpawnPlayerEvent.RegisterListener(OnSpawnPlayerEvent);
    }

    private void OnSpawnPlayerEvent(SpawnPlayerEvent _)
    {
        var rectTransform = GetComponent<RectTransform>();
        float topOfContainer = rectTransform.rect.height / 2;
        float spawnLocationXCoordinate = rectTransform.rect.width / 4;
        float scaledSpawnLocationYOffset = (rectTransform.rect.height * (PercentOfAvailableVerticalScreenSpace / 100f)) / 2;
        float spawnLocationYCoordinate = topOfContainer - scaledSpawnLocationYOffset;
        Instantiate(PlayerPrefab, new Vector3(-spawnLocationXCoordinate, spawnLocationYCoordinate), Quaternion.identity, transform);
    }
}
