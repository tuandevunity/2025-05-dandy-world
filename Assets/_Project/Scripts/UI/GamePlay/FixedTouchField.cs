using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : TickBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 TouchDist;
    private Vector2 pointerOld;
    private int pointerId;
    private bool _isActive = false;
    [SerializeField] private bool pressed;
    protected override void OnDisable()
    {
        base.OnDisable();
        StopTouch();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (pressed)
        {
            if (Time.timeScale == 0)
            {
                TouchDist = new Vector2();
            }
            else
            {
                if (pointerId >= 0 && pointerId < Input.touches.Length)
                {
                    TouchDist = Input.touches[pointerId].position - pointerOld;
                    pointerOld = Input.touches[pointerId].position;
                }
                else
                {
                    TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointerOld;
                    pointerOld = Input.mousePosition;
                }
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        pointerId = eventData.pointerId;
        pointerOld = eventData.position;
        if (_isActive) return;
        _isActive = true;
        UIManager.HidePanel<PanelTutorialRotate>();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
    public void StopTouch()
    {
        pressed = false;
        TouchDist = new Vector2();
    }
}