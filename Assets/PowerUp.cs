using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Scriptable Objects/PowerUp")]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public Sprite powerUpIcon;
    public GameObject powerUpProp;
}