using Entities.Events;
using UnityEngine;
using UnityEngine.UI;

public class GainMaxHealthButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CharacterGainMaxHealthEvent(1).Fire();
    }
}
