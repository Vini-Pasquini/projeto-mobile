using UnityEngine;

[RequireComponent(typeof(CutsceneHandler))]
public class CutsceneInitiator : MonoBehaviour
{
    private CutsceneHandler cutsceneHandler;

    private void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cutsceneHandler.PlayNextElement();
        }
    }
}
