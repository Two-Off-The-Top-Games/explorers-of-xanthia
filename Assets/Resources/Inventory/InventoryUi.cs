using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject TEMPHealthPotion;
    private List<GameObject> _itemActionButtons = new List<GameObject>();
    private List<GameObject> _items = new List<GameObject>();

    private void Start()
    {
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
        Instantiate(TEMPHealthPotion, transform);
    }

    // TODO: Register listeners
}
