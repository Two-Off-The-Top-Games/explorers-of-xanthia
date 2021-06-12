using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject ItemActionsPanel;
    public GameObject ItemActionButtonPrefab;
    private List<GameObject> _itemActionButtons = new List<GameObject>();
    private List<GameObject> _items = new List<GameObject>();
    
    void OnUpdate()
    {
        ItemActionsPanel.SetActive(false);
    }

    // TODO: Register listeners
}
