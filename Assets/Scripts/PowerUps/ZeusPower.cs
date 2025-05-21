using UnityEngine;

public class ZeusPower : PowerUpProp
{
    private Transform _target;

    private void Start()
    {
        this._target = GameObject.Find("Minotaur").transform;
    }

    public override void Update()
    {
        if (this._target != null)
        {
            this.transform.LookAt(this._target, Vector3.back);
        }

        base.Update();
    }
}
