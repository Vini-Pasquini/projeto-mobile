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

    private int _currentDraggedCardIndex = -1;
    private int _currentCardDragFingerID = -1;

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
        this.TryDragStart(touchIndex);

        // debug
        switch (touchIndex)
        {
            case 4:
                this.SpawnCard();
                break;
        }
    }

    private void TouchPhase_Moved(int touchIndex)
    {
        this.UpdateCardDrag(touchIndex);
    }

    private void TouchPhase_Stationary(int touchIndex)
    {

    }

    private void TouchPhase_Ended(int touchIndex)
    {
        this.CheckDragEnd(touchIndex);
    }

    private void TouchPhase_Canceled(int touchIndex)
    {
        this.CheckDragEnd(touchIndex);
    }

    /* Card Drag and Drop */

    private void TryDragStart(int touchIndex)
    {
        if (this._currentDraggedCardIndex >= 0) return;

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.touches[touchIndex].position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Where(r => r.gameObject.layer == 5).Count() <= 0) return;

        for (int draggedCardIndex = 0; draggedCardIndex < this._draggableCards.Count; draggedCardIndex++)
        {
            if (results[0].gameObject != (this._draggableCards[draggedCardIndex] as Card).CardObject) continue;
            this._draggableCards[draggedCardIndex].OnDragStart();
            this._currentDraggedCardIndex = draggedCardIndex;
            this._currentCardDragFingerID = Input.touches[touchIndex].fingerId;
            break;
        }
    }

    private void UpdateCardDrag(int touchIndex)
    {
        if (this._currentDraggedCardIndex < 0 || Input.touches[touchIndex].fingerId != this._currentCardDragFingerID) return;

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.touches[touchIndex].position;
        this._draggableCards[this._currentDraggedCardIndex].OnDragUpdate(eventData);
    }

    private void CheckDragEnd(int touchIndex)
    {
        if (this._currentDraggedCardIndex < 0 || Input.touches[touchIndex].fingerId != this._currentCardDragFingerID) return;

        List<RaycastHit2D> results = new List<RaycastHit2D>();
        Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.touches[touchIndex].position), float.MaxValue, results);

        // TODO: mudar pra tag depois
        if (results.Where(hit => hit.rigidbody.gameObject.name.StartsWith("Door")).Count() <= 0)
        {
            this._draggableCards[this._currentDraggedCardIndex].OnDragEnd();

            this._currentDraggedCardIndex = -1;
            this._currentCardDragFingerID = -1;
            return;
        }

        // TODO: verificar se porta condiz com letra na carta (precisa implementar isso ainda)
        Card currentCard = this._draggableCards[this._currentDraggedCardIndex] as Card;
        this._draggableCards.RemoveAt(this._currentDraggedCardIndex);
        currentCard.ConsumeCard();

        this.SpawnCard();

        this._currentDraggedCardIndex = -1;
        this._currentCardDragFingerID = -1;
        return;
    }

    private void SpawnCard()
    {
        GameObject newCardSlot = Instantiate(this._cardSlotPrefab, this._cardSlotPanel);
        Card newCard = new Card(newCardSlot);
        this._draggableCards.Add(newCard);
    }
}
