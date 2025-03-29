using UnityEngine;

[CreateAssetMenu(fileName = "LibrasSign", menuName = "Scriptable Objects/LibrasSign")]
public class LibrasSign : ScriptableObject
{
    [SerializeField] private string _text;
    [SerializeField] private Sprite _sign;

    public string Text { get { return _text; } }
    public Sprite Sign { get { return _sign; } }
}
