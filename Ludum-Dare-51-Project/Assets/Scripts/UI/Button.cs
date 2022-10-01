using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite unPressed, pressed;
    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = unPressed;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = pressed;
    }

    public void ButtonClicked()
    {
        Debug.Log("Clicked");
    }
}
