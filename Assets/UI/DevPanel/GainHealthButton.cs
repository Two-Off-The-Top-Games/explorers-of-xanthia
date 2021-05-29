using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class GainHealthButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CharacterGainHealthEvent(1).Fire();
    }
}
