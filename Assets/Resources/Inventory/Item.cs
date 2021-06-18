using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public GameObject ItemActionButton;
    public GameObject ItemActionPanel;
    public TextMeshProUGUI ItemText;

    protected int OwnerId;
    public string Name;
    protected abstract List<ItemAction> Actions { get; }

    private GameObject _spawnedItemActionPanel;
    private Button _button;

    private void Start()
    {
        Debug.Log("I am starting!");
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        ItemText.text = Name;
    }

    private void OnButtonClicked()
    {
        Debug.Log("I was clicked!");
        SpawnItemActionPanel();
    }

    private void SpawnItemActionPanel()
    {
        _spawnedItemActionPanel = Instantiate(ItemActionPanel, transform, false);
        foreach(var action in Actions)
        {
            var spawnedItemActionButton = Instantiate(ItemActionButton, _spawnedItemActionPanel.transform, false);
            spawnedItemActionButton.GetComponentInChildren<TextMeshProUGUI>().text = action.Name;
        }
    }

    private void DestroyItemActionPanel()
    {
        Destroy(_spawnedItemActionPanel);
    }
}
