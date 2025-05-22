using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public float yOffset;
    private float cameraYOffset = 0f;

    public Vector2 maxPos;
    public Vector2 minPos;

    private void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y + cameraYOffset, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }

        if (Camera.main.orthographicSize > 1)
            cameraYOffset = yOffset;
        else
            cameraYOffset = 0f;
    }
}
