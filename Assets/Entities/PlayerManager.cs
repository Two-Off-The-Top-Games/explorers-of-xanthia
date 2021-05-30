using GameState.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private void OnEnable()
    {
        SpawnPlayerEvent.RegisterListener(OnSpawnPlayerEvent);
    }

    private void OnSpawnPlayerEvent(SpawnPlayerEvent _)
    {
        var rectTransform = GetComponent<RectTransform>();
        float spawnLocationXCoordinate = rectTransform.rect.width / 4;
        Instantiate(PlayerPrefab, new Vector3(-spawnLocationXCoordinate, 0), Quaternion.identity, transform);
    }
}
