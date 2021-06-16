using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject TEMPHealthPotion;
    public GameObject ItemActionsPanel;
    public GameObject ItemActionButtonPrefab;
    private List<GameObject> _itemActionButtons = new List<GameObject>();
    private List<GameObject> _items = new List<GameObject>();
    
    void OnUpdate()
    {
        ItemActionsPanel.SetActive(false);
    }

    private void Start()
    {
        Instantiate(TEMPHealthPotion, new Vector3(0, 0), Quaternion.identity, transform);
        Instantiate(TEMPHealthPotion, new Vector3(0, 100), Quaternion.identity, transform);
        Instantiate(TEMPHealthPotion, new Vector3(0, 200), Quaternion.identity, transform);
        Instantiate(TEMPHealthPotion, new Vector3(0, 300), Quaternion.identity, transform);
        Instantiate(TEMPHealthPotion, new Vector3(0, 400), Quaternion.identity, transform);
        Instantiate(TEMPHealthPotion, new Vector3(0, 600), Quaternion.identity, transform);
        Instantiate(TEMPHealthPotion, new Vector3(0, 700), Quaternion.identity, transform);
    }

    // TODO: Register listeners
}
