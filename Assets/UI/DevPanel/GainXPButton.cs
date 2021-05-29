using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class GainXPButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CharacterGainXPEvent(10).Fire();
    }
}
