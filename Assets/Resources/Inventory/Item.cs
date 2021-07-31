using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public TextMeshProUGUI ItemText;

    protected int OwnerId;
    public string Name;
    protected abstract Action UseAction { get; }

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        ItemText.text = Name;
    }

    private void OnButtonClicked()
    {
        UseItemAction();
    }

    private void UseItemAction()
    {
        UseAction();
    }
}
