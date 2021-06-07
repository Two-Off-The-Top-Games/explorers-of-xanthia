using Entities.Events;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    //[HideInInspector]
    //public int CharacterInstanceId;

    private void OnEnable()
    {
        Instance = this;
        RegisterCharacterInstanceIdEvent.RegisterListener(OnRegisterCharacterInstanceIdEvent);
    }

    private void OnRegisterCharacterInstanceIdEvent(RegisterCharacterInstanceIdEvent registerCharacterInstanceIdEvent)
    {
        //CharacterInstanceId = registerCharacterInstanceIdEvent.CharacterInstanceId;
    }
}
