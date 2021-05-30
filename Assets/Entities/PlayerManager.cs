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
        Instantiate(PlayerPrefab, new Vector3(-5, 0), Quaternion.identity, transform);
    }
}
