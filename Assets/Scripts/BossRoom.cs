using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public bool playerEntered = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEntered = true;
            //GameManager.Instance.EnterBossRoom();
        }
    }
}
