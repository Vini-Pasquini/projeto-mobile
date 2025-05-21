using UnityEngine;

public class PowerUpProp : MonoBehaviour
{
    protected bool _isPermanent;
    protected float _lifeTime;
    protected float _elapsedTime;

    public void InitPowerUp(float lifeTime = -1f)
    {
        this._lifeTime = lifeTime;
        this._isPermanent = lifeTime <= 0f;
    }

    public virtual void Update()
    {
        this.Timer();
    }

    private void Timer()
    {
        if (this._isPermanent) return;

        this._elapsedTime += Time.deltaTime;
        if (this._elapsedTime >= this._lifeTime)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
