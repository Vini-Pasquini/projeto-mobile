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

    protected override void Awake()
    {
        base.Awake();
        this._librasLetterSigns = Resources.LoadAll<LibrasSign>("LibrasSigns/Letters");
        player = GameObject.FindWithTag("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        door = GameObject.FindWithTag("Spawndoor");
        door.SetActive(false);

        this.spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
    }

    private void Start()
    {
        int spawn = Random.Range(0, spawnpoints.Length);
        player.transform.position = spawnpoints[spawn].transform.position;
        Debug.Log("Player nasceu no spawn " + spawn);
    }

    // TODO: implementar track de dup
    public LibrasSign GetRandomLibrasSign()
    {
        return this._librasLetterSigns[Random.Range(0, this._librasLetterSigns.Length)];
    }

    public void EnterBossRoom()
    {
        Debug.Log("Encontrou o Minotauro");
        GameObject.Find("Canvas").GetComponent<UIController>().ActivateGameOver();
        playerCtrl.SetIsActive(false);
    }
}
