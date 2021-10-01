using Events;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    ActiveCharacters _activeCharacters = new ActiveCharacters();

    public void Awake()
    {
        DataSource<ActiveCharacters>.ProvideSingle(_activeCharacters);
        // This will ensure that the requester of this data source will receive the proper thread local instance of Random.
        DataSource<ThreadSafeRandom>.ProvideSingle(new ThreadSafeRandom());
    }
}
