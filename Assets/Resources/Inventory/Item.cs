using Events.Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Clickable))]
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
    private Button _button;
    private Clickable _clickable;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        _clickable = GetComponent<Clickable>();
        _clickable.InvokeClick = OnButtonClicked;
        ItemText.text = Name;
    }

    private void OnButtonClicked()
    {
        SpawnItemActionPanel();
    }

    private void SpawnItemActionPanel()
    {
        _spawnedItemActionPanel = Instantiate(ItemActionPanel, transform, false);
        EventSystem.current.SetSelectedGameObject(_spawnedItemActionPanel);
        SpawnOutsideClickDetector();
        foreach (var action in Actions)
        {
            var spawnedItemActionButton = Instantiate(ItemActionButton, _spawnedItemActionPanel.transform, false);
            spawnedItemActionButton.GetComponentInChildren<TextMeshProUGUI>().text = action.Name;
            var buttonOnClick = spawnedItemActionButton.GetComponent<Button>().onClick;
            buttonOnClick.AddListener(() =>
            {
                action.Action();
                CleanUp();
            });
            spawnedItemActionButton.GetComponent<Clickable>().InvokeClick = () => buttonOnClick.Invoke();
        }
    }

    private void SpawnOutsideClickDetector()
    {
        Instantiate(OutsideClickDetector, transform.root, false);
        new RegisterOutsideClickEvent(DestroySpawnedItemActionPanel).Fire();
    }

    private void DestroySpawnedItemActionPanel()
    {
        Debug.Log("Destroying spawned item action panel.");
        Destroy(_spawnedItemActionPanel);
    }

    private void CleanUp()
    {
        DestroySpawnedItemActionPanel();
        Destroy(gameObject);
    }
}
