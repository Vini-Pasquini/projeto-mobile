using UnityEngine;

public class CutsceneHandler : MonoBehaviour
{
    public Camera cam;
    public PlayerController player;

    public bool isReplayable = false;
    public bool allowPlayerMovement = false;

    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;

    private void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void ExecuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
        {
            if (!allowPlayerMovement)
                player.SetIsActive(false);
            cutsceneElements[index].Execute();
        }
        else
        {
            player.SetIsActive(true);
        }

        if (isReplayable && index >= cutsceneElements.Length)
        {
            index = -1;
        }
    }

    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }
}
