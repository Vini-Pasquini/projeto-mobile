using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _cardSlotPanel;
    [SerializeField] private GameObject _cardSlotPrefab;

    private List<IDraggable> _draggableCards = new List<IDraggable>();

    private void Update()
    {
        this.TouchHandler();
    }

    private void TouchHandler()
    {
        for (int index = 0; index < Input.touches.Length; index++)
        {
            switch (Input.touches[index].phase)
            {
                case TouchPhase.Began: this.TouchPhase_Began(index); break;
                case TouchPhase.Moved: this.TouchPhase_Moved(index); break;
                case TouchPhase.Stationary: this.TouchPhase_Stationary(index); break;
                case TouchPhase.Ended: this.TouchPhase_Ended(index); break;
                case TouchPhase.Canceled: this.TouchPhase_Canceled(index); break;
            }
        }
    }

    private void TouchPhase_Began(int touchIndex)
    {
        switch (touchIndex)
        {
            case 0:
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.touches[touchIndex].position;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Where(r => r.gameObject.layer == 5).Count() <= 0) break;

                for (int i = 0; i < this._draggableCards.Count; i++)
                {
                    if (results[0].gameObject != (this._draggableCards[i] as Card).CardObject) continue;
                    this._draggableCards[i].OnDragStart();
                    break;
                }
                Debug.Log(results[0].gameObject.name);
                break;

            case 1:
                this.SpawnCard();
                break;
        }
    }

    private void TouchPhase_Moved(int touchIndex)
    {
        switch (touchIndex)
        {
            case 0:
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.touches[touchIndex].position;

                for (int i = 0; i < this._draggableCards.Count; i++) { this._draggableCards[i].OnDragUpdate(eventData); }
                break;
        }
    }

    private void TouchPhase_Stationary(int touchIndex)
    {

    }

    private void TouchPhase_Ended(int touchIndex)
    {
        switch (touchIndex)
        {
            case 0:
                for (int i = 0; i < this._draggableCards.Count; i++) { this._draggableCards[i].OnDragEnd(); }
                break;
        }
    }

    private void TouchPhase_Canceled(int touchIndex)
    {
        switch (touchIndex)
        {
            case 0:
                for (int i = 0; i < this._draggableCards.Count; i++) { this._draggableCards[i].OnDragEnd(); }
                break;
        }
    }

    private void SpawnCard()
    {
        GameObject newCardSlot = Instantiate(this._cardSlotPrefab, this._cardSlotPanel);
        Card newCard = new Card(newCardSlot);
        this._draggableCards.Add(newCard);
    }
}
