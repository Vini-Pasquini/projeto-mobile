using UnityEngine;

public class BossRoom : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.EnterBossRoom();
        }
    }
}
