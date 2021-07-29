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
        _onOutsideClickAction = registerOutsideClickEvent.OnOutsideClickAction;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _onOutsideClickAction();

        var raycaster = transform.parent.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
            //if (result.gameObject.GetComponent<Selectable>())
            //{
            //    ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            //}
        }
        Destroy(gameObject);
    }
}
