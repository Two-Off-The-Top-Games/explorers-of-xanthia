using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public GameObject ItemActionButton;
    public GameObject ItemActionPanel;
    protected int OwnerId;
    protected abstract List<ItemAction> Actions { get; }
    private void OnMouseDown()
    {
        // TODO: Open item actions menu.
    }
}
