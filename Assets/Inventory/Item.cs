using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public List<IItemAction> Actions;
    private void OnMouseDown()
    {
        // TODO: Open item actions menu.
    }
}
