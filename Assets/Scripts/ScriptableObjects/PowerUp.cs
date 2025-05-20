using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Scriptable Objects/PowerUp")]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public Sprite powerUpIcon;
    public GameObject powerUpProp;
    [Tooltip("equal or less than 0 lifeTime makes powerup permanent")]
    public float lifeTime;

    public void Cast(Transform caster)
    {
        GameObject newProp = GameObject.Instantiate(powerUpProp, caster.position, Quaternion.identity);
        newProp.GetComponent<PowerUpProp>().InitPowerUp(lifeTime);
    }
}