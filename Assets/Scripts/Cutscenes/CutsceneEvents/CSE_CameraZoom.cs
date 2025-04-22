using System.Collections;
using UnityEngine;

public class CSE_CameraZoom : CutsceneElementBase
{
    [SerializeField] private float targetSize;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f,0f,-10f);
    private Camera cam;

    public override void Execute()
    {
        cam = cutsceneHandler.cam;
        StartCoroutine(ZoomCamera());
    }

    private IEnumerator ZoomCamera()
    {
        Vector3 originalPosition = cam.transform.position;
        if (target == null)
            target = cam.transform;
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = -10f;

        float originalSize = cam.orthographicSize;
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            cam.orthographicSize = Mathf.Lerp(originalSize, targetSize, t);
            cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        cam.orthographicSize = targetSize;
        cam.transform.position = targetPosition;

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
