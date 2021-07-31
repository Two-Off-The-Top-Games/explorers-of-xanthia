using Events.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutsideClickDetector : MonoBehaviour, IPointerDownHandler
{
    private Action _onOutsideClickAction = () => { };

    private void Awake()
    {
        RegisterOutsideClickEvent.RegisterListener(OnRegisterOutsideClickEvent);
    }

    private void OnDestroy()
    {
        RegisterOutsideClickEvent.DeregisterListener(OnRegisterOutsideClickEvent);
    }

    private void OnRegisterOutsideClickEvent(RegisterOutsideClickEvent registerOutsideClickEvent)
    {
        Debug.Log("Registered onOutsideClickAction.");
        _onOutsideClickAction = registerOutsideClickEvent.OnOutsideClickAction;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Clickable clickable = null;
        var raycaster = transform.parent.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        foreach (RaycastResult result in results)
        {
            clickable = result.gameObject.GetComponent<Clickable>();
            if (clickable != null)
            {
                break;
            }
        }

        _onOutsideClickAction();
        clickable?.InvokeClick();
        Destroy(gameObject);
    }
}
