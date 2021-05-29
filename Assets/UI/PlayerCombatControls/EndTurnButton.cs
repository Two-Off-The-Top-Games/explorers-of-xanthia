using GameState.Events;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new EndTurnEvent().Fire();
    }
}
