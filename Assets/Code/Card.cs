using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : IDraggable
{ 
    private GameObject _currentSlot;
    private GameObject _cardObject;
    private Image _cardImage;

    public GameObject CardObject { get { return this._cardObject; } }

    private bool _isDragging;

    public Card(GameObject cardSlot)
    {
        this._currentSlot = cardSlot;
        this._cardObject = this._currentSlot.transform.GetChild(0).gameObject;
        this._cardImage = this._cardObject.GetComponent<Image>();

        this._isDragging = false;
    }

    public void OnDragStart()
    {
        this._isDragging = true;
        this._cardImage.raycastTarget = false;
    }
    
    public void OnDragUpdate(PointerEventData eventData)
    {
        if (!this._isDragging) return;
        this._cardObject.transform.position = eventData.position;
    }

    public void OnDragEnd()
    {
        this._cardObject.transform.position = this._currentSlot.transform.position;
        this._cardImage.raycastTarget = true;
        this._isDragging = false;
    }
}
