using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<int, int> OnButtonHover;
    public event Action<int, int> OnButtonHoverEnd;

    [SerializeField] int _buttonNumber;
    [SerializeField] int _side;
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        OnButtonHover?.Invoke(_buttonNumber, _side);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        OnButtonHoverEnd?.Invoke(_buttonNumber, _side);
    }
}
