using System.Linq;
using UnityEngine;

public class GameManager : IPersistentSingleton<GameManager>
{
    private LibrasSign[] _librasLetterSigns;
    private LibrasSign[] _librasWordSigns;

    [SerializeField] private GameObject[] spawnpoints;

    public GameObject door;
    private GameObject player;
    private PlayerController playerCtrl;

    private UIController _currentUIController;

    private ShowInterstitial _adController;

    protected override void Awake()
    {
        base.Awake();

        this._librasLetterSigns = Resources.LoadAll<LibrasSign>("LibrasSigns/Letters");
        this._librasWordSigns = Resources.LoadAll<LibrasSign>("LibrasSigns/Words");

        player = GameObject.FindWithTag("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        door = GameObject.FindWithTag("Spawndoor");
        door.SetActive(false);

        this.spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

        this._currentUIController = GameObject.Find("Canvas").GetComponent<UIController>(); // CASO MUDAR DE CENA, ACHAR UICONTROLLER DNV
    }

    private void Start()
    {
        int spawn = Random.Range(0, spawnpoints.Length);
        player.transform.position = spawnpoints[spawn].transform.position;
        Debug.Log("Player nasceu no spawn " + spawn);

        this._adController = new GameObject(typeof(ShowInterstitial).Name).AddComponent<ShowInterstitial>();
    }

    public LibrasSign GetRandomLibrasLetterSign()
    {
        return this._librasLetterSigns[Random.Range(0, this._librasLetterSigns.Length)];
    }

    public LibrasSign GetRandomLibrasWordSign()
    {
        return this._librasWordSigns[Random.Range(0, this._librasWordSigns.Length)];
    }

    public void EnterBossRoom()
    {
        Debug.Log("Encontrou o Minotauro");
        this._currentUIController.ActivateGameOver();
        playerCtrl.SetIsActive(false);
    }

    private GameObject _chestInRange = null;
    public GameObject ChestInRange { get { return this._chestInRange; } }

    private ChestController _chestController = null;
    public ChestController ChestController
    {
        get
        {
            if (this._chestController == null) { this._chestController = this._chestInRange.GetComponent<ChestController>(); }
            return this._chestController;
        }
    }

    public bool RegisterChestInRange(GameObject chest)
    {
        if (this._chestInRange != null) return false;

        this._chestInRange = chest;
        this._currentUIController.SetInteractionButtonState(true);
        return true;
    }

    public bool UnregisterChestInRange(GameObject chest)
    {
        if (this._chestInRange == null || this._chestInRange != chest) return false;

        this._chestInRange = null;
        this._chestController = null;
        this._currentUIController.SetInteractionButtonState(false);
        return true;
    }

    private PowerUp _powerUpSlot = null;
    public PowerUp PowerUpSlot { get { return this._powerUpSlot; } }

    public void SetPowerUpSlot(PowerUp reward)
    {
        if (this._powerUpSlot != null) return; // TODO: dar opcao de trocar o powerup equipado
        this._powerUpSlot = reward;
        this._currentUIController.UpdatePowerUpIcon();
    }

    public void UsePowerUp()
    {
        if (this._powerUpSlot == null) return;

        float timer = this._powerUpSlot.Cast(player.transform);
        this._powerUpSlot = null;
        this._currentUIController.UpdatePowerUpIcon(timer);
    }

    public void CallBossPuzzle(LibrasSign setAnswer = null)
    {
        Debug.Log("GM CALL");

        if (this._currentUIController.QuestionPuzzleController == null) return;

        LibrasSign answerWord = setAnswer != null ? setAnswer : this.GetRandomLibrasWordSign();

        this._currentUIController.QuestionPuzzleController.StartLibrasPuzzle(answerWord, QuestionPuzzleMode.BossFight);
    }

    int puzzleAnswer = -1;

    public int GetPuzzleAnswer
    {
        get
        {
            int answer = puzzleAnswer;
            puzzleAnswer = -1;
            return answer;
        }
    }

    public void PuzzleAnswer(bool answer)
    {
        puzzleAnswer = answer ? 1 : 0;
    }

    public void PlayerDeath()
    {
        Debug.Log("Player morreu");
        GameObject.Find("Canvas").GetComponent<UIController>().ActivateGameOver();
        playerCtrl.SetIsActive(false);
    }

    private bool _hasUnlockedDic = false;

    public bool HasUnlockedDic { get { return this._hasUnlockedDic; } }

    public void UnlockDictionary()
    {
        this._hasUnlockedDic = true;
        this._currentUIController.OnDictionaryButtonPress();
    }

    public void WatchAd()
    {
        this._adController.ShowRewardedAd();
    }
}
