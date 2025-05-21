using UnityEngine;

public class HermesPower : PowerUpProp
{
    private PlayerController _player;

    private void Start()
    {
        this._player = GameObject.Find("Player").GetComponent<PlayerController>();
        this._player.SetSpeedMultiplier(1.5f);
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        this._player.SetSpeedMultiplier(1f);
    }
}
