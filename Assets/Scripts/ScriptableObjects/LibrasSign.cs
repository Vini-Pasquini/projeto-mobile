using UnityEngine;

[CreateAssetMenu(fileName = "LibrasSign", menuName = "Scriptable Objects/LibrasSign")]
public class LibrasSign : ScriptableObject
{
    [SerializeField] private string _signText;
    [SerializeField] private Sprite _signSprite;

    public string SignText { get { return _signText; } }
    public Sprite SignSprite { get { return _signSprite; } }
}
