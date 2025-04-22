using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Card : IDraggable
{
    private LibrasSign _librasSign;

    private GameObject _currentSlot;
    private GameObject _cardObject;
    private Image _cardBackground;
    private TextMeshProUGUI _cardText;

    public LibrasSign LibrasSign { get { return this._librasSign; } }
    public GameObject CardObject { get { return this._cardObject; } }

    private bool _isDragging;

    public Card(GameObject cardSlot, LibrasSign librasSign)
    {
        this._librasSign = librasSign;
        
        this._currentSlot = cardSlot;
        this._cardObject = this._currentSlot.transform.GetChild(0).gameObject;
        this._cardBackground = this._cardObject.GetComponent<Image>();
        this._cardText = this._cardObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        this._cardText.text = this._librasSign.SignText;

        this._isDragging = false;
    }

    public void OnDragStart()
    {
        this._isDragging = true;
        this._cardBackground.raycastTarget = false;
    }
    
    public void OnDragUpdate(PointerEventData eventData)
    {
        if (!this._isDragging) return;
        this._cardObject.transform.position = eventData.position;
    }

    public void OnDragEnd()
    {
        this._cardObject.transform.position = this._currentSlot.transform.position;
        this._cardBackground.raycastTarget = true;
        this._isDragging = false;
    }

    public void ConsumeCard()
    {
        // TODO: fazer uma animacao para destruir a carta
        GameObject.Destroy(this._currentSlot);
    }
}
