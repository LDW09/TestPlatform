using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HoldButton : Button, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onHold { get; private set; } = new UnityEvent();
    public UnityEvent onRelease { get; private set; } = new UnityEvent();

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        onHold.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onRelease.Invoke();
    }

    protected override void OnDestroy()
    {
        onHold.RemoveAllListeners();
        onRelease.RemoveAllListeners();
    }
}
