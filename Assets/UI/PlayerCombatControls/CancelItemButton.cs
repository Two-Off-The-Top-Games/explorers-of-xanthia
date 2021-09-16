using Events.UI;
using UnityEngine;
using UnityEngine.UI;

public class CancelItemButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        new CancelButtonClickedEvent().Fire();
    }
}
