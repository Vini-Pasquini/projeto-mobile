using System.Collections;
using UnityEngine;

public class CSE_Door : CutsceneElementBase
{
    private Camera cam;
    [SerializeField] private Vector2 distanceToMove;

    [SerializeField] private GameObject door;
    public override void Execute()
    {
        cam = cutsceneHandler.cam;
        door = GameManager.Instance.door;
        StartCoroutine(PanToDoorCoroutine());
    }

    private IEnumerator PanToDoorCoroutine()
    {
        Vector3 originalPosition = cam.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(distanceToMove.x, distanceToMove.y, 0f);
        originalPosition.x = this.transform.position.x;

        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        cam.transform.position = targetPosition;
        door.SetActive(true);

        startTime = Time.time;
        elapsedTime = 0f;

        while (elapsedTime < waitDuration)
        {
            cam.transform.position = targetPosition;
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
