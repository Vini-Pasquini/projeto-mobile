using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CutsceneHandler))]
public class CutsceneElementBase : MonoBehaviour
{
    public float duration = 1f;
    public float waitDuration = 0f;
    public CutsceneHandler cutsceneHandler { get; private set; }

    private void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute()
    {

    }

    protected IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }
}
