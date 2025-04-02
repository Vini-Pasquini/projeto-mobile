using System.Linq;
using UnityEngine;

public class GameManager : IPersistentSingleton<GameManager>
{
    private LibrasSign[] _librasLetterSigns;
    private LibrasSign[] _librasWordSigns;

    private PlayerController player;

    protected override void Awake()
    {
        base.Awake();
        this._librasLetterSigns = Resources.LoadAll<LibrasSign>("LibrasSigns/Letters");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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
        player.SetIsActive(false);
    }
}
