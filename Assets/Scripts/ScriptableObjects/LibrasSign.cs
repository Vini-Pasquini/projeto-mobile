using UnityEngine;

[CreateAssetMenu(fileName = "LibrasSign", menuName = "Scriptable Objects/LibrasSign")]
public class LibrasSign : ScriptableObject
{
    [SerializeField] private string _signText;
    [SerializeField] private Sprite _signSprite;

    [SerializeField] private string _disctionaryWord; // TODO: inverter com _signText
    [SerializeField] private string _originGod;
    [SerializeField] private string _extra;

    public string SignText { get { return _signText; } }
    public Sprite SignSprite { get { return _signSprite; } }

    public string DictionaryWord { get { return _disctionaryWord; } }
    public string OriginGod { get { return _originGod; } }
    public string Extra { get { return _extra; } }
}
