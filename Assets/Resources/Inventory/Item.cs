using Events.Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public GameObject ItemActionButton;
    public GameObject ItemActionPanel;
    public GameObject OutsideClickDetector;
    public TextMeshProUGUI ItemText;

    protected int OwnerId;
    public string Name;
    protected abstract List<ItemAction> Actions { get; }

    private GameObject _spawnedItemActionPanel;
    private GameObject _spawnedOutsideClickDetector;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        ItemText.text = Name;
    }

    private void OnButtonClicked()
    {
        SpawnItemActionPanel();
    }

    private void SpawnItemActionPanel()
    {
        _spawnedItemActionPanel = Instantiate(ItemActionPanel, transform, false);
        SpawnOutsideClickDetector();
        foreach (var action in Actions)
        {
            var spawnedItemActionButton = Instantiate(ItemActionButton, _spawnedItemActionPanel.transform, false);
            spawnedItemActionButton.GetComponentInChildren<TextMeshProUGUI>().text = action.Name;
            spawnedItemActionButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                action.Action();
                CleanUp();
            });
        }
    }

    private void SpawnOutsideClickDetector()
    {
        _spawnedOutsideClickDetector = Instantiate(OutsideClickDetector, transform.root, false);
        new RegisterOutsideClickEvent(DestroySpawnedItemActionPanel).Fire();
    }

    private void DestroySpawnedItemActionPanel()
    {
        Destroy(_spawnedItemActionPanel);
    }

    private void CleanUp()
    {
        DestroySpawnedItemActionPanel();
        Destroy(gameObject);
    }
}
