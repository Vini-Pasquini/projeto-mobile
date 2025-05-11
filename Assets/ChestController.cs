using UnityEngine;

public class ChestController : MonoBehaviour
{
    private LibrasSign _puzzleSolution;
    public LibrasSign PuzzleSolution { get { return this._puzzleSolution; } }

    private bool _solved = false;

    private void Start()
    {
        this._puzzleSolution = GameManager.Instance.GetRandomLibrasWordSign(); // PH
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !this._solved)
        {
            GameManager.Instance.RegisterChestInRange(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !this._solved)
        {
            GameManager.Instance.UnregisterChestInRange(this.gameObject);
        }
    }

    public void DisableChestPuzzle()
    {
        this._solved = true;
        this.GetComponent<SpriteRenderer>().sprite = open; // PH
        GameManager.Instance.UnregisterChestInRange(this.gameObject);
    }


    [SerializeField] private Sprite open; // PH
}
