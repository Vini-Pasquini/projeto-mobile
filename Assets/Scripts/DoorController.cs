using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private LibrasSign _librasSignLock;

    private void Start()
    {
        this._librasSignLock = GameManager.Instance.GetRandomLibrasSign();

        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this._librasSignLock.SignSprite;
    }

    public bool CheckLibrasCardMatch(LibrasSign librasSignKey)
    {
        bool passFlag =  this._librasSignLock == librasSignKey;
        
        this.gameObject.SetActive(!passFlag); // ph
        
        return passFlag;
    }
}
