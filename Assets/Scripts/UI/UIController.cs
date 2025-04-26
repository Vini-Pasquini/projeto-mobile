using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _cardSlotPanel;
    [SerializeField] private GameObject _cardSlotPrefab;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject vignette;

    private List<IDraggable> _draggableCards = new List<IDraggable>();

    private const int MAX_HAND_CAPACITY = 8;

    private int _currentDraggedCardIndex = -1;
    private int _currentCardDragFingerID = -1;

    private List<LibrasSign> _oldCardHandBuffer = new List<LibrasSign>();
    private List<LibrasSign> _newCardHandBuffer = new List<LibrasSign>();

    private void Start()
    {
        vignette.SetActive(true);
        tutorialPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        this.DrawFullCardHand();
    }

    private void Update()
    {
        this.TouchHandler();
    }

    // Buttons

    public void PlayGame()
    {
        SceneManager.LoadScene("TestChar");
    }

    public void TestCards()
    {
        SceneManager.LoadScene("TestFullUI");
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    /* Touch Input Stuff */

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

        if (results.Where(hit => hit.rigidbody.gameObject.CompareTag("Door")).Count() <= 0)
        {
            this._draggableCards[this._currentDraggedCardIndex].OnDragEnd();
            this._currentDraggedCardIndex = this._currentCardDragFingerID = -1;
            return;
        }

        DoorController door = results[0].collider.GetComponent<DoorController>();
        Debug.Assert(door != null, "[!] Missing <DoorController> Component");

        Card currentCard = this._draggableCards[this._currentDraggedCardIndex] as Card;

        if (door.CheckLibrasCardMatch(currentCard.LibrasSign))
        {
            this._draggableCards.RemoveAt(this._currentDraggedCardIndex);
            currentCard.ConsumeCard();
            this.SpawnLibrasCard();
        }
        else { this._draggableCards[this._currentDraggedCardIndex].OnDragEnd(); }

        this._currentDraggedCardIndex = this._currentCardDragFingerID = -1;

        return;
    }

    /* Outros */

    public void OnResetCardsButtonPress()
    {
        for (int i = 0; i < this._draggableCards.Count; i++) { (this._draggableCards[i] as Card).ConsumeCard(); }

        this._draggableCards.Clear();
        
        this.DrawFullCardHand();
    }

    private void DrawFullCardHand()
    {
        for (int i = 0; i < MAX_HAND_CAPACITY; i++) { this.SpawnLibrasCard(); }
        this._oldCardHandBuffer.Clear();

        for (int i = 0; i < this._newCardHandBuffer.Count; i++) { this._oldCardHandBuffer.Add(this._newCardHandBuffer[i]); }
        
        this._newCardHandBuffer.Clear();
    }

    private void SpawnLibrasCard()
    {
        LibrasSign newLibrasSign = GameManager.Instance.GetRandomLibrasSign();

        while (this._oldCardHandBuffer.Contains(newLibrasSign) || this._newCardHandBuffer.Contains(newLibrasSign))
        {
            newLibrasSign = GameManager.Instance.GetRandomLibrasSign();
        }

        this._newCardHandBuffer.Add(newLibrasSign);

        GameObject newCardSlot = Instantiate(this._cardSlotPrefab, this._cardSlotPanel);
        Card newCard = new Card(newCardSlot, newLibrasSign);
        this._draggableCards.Add(newCard);
    }


    public void ActivateGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
