using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Objects")]
    public Transform A;
    public Transform B;

    [Header("Parameters")]
    public Vector3 cameraOffset;
    public float cameraFollowSpeed;
    public float cameraLookSpeed;
    public float cameraZoomMult;
    public float minZoom = 3;

    public float minX = 0f;
    public float minHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      var target = (A.position + B.position) / 2f;
      var dist = (A.position - B.position).magnitude;
      if (dist < minZoom) dist = minZoom;
      var zoomMult = dist * cameraZoomMult;
      var targetPosition = target + (cameraOffset*zoomMult);
      if (targetPosition.x < minX) targetPosition = new Vector3(minX, targetPosition.y, targetPosition.z);
      //if (targetPosition.y < minHeight) targetPosition = new Vector3(targetPosition.x, minX, targetPosition.z);
      transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target-transform.position), Time.deltaTime * cameraLookSpeed);
    }
}
