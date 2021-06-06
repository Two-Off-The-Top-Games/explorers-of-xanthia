using GameState.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
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
        float spawnLocationYOffset = (rectTransform.rect.height * PercentOfAvailableVerticalScreenSpace) / 2;
        float spawnLocationYCoordinate = topOfContainer - spawnLocationYOffset;
        Instantiate(PlayerPrefab, new Vector3(-spawnLocationXCoordinate, spawnLocationYCoordinate), Quaternion.identity, transform);
    }
}
