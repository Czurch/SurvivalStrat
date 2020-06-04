using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera cam;
    private float distFromCamera;
    private Vector3 mouseOffset;
    private Plane objPlane;
    public bool isDragging;
    void Start()
    {
        objPlane = new Plane(Vector3.up, gameObject.transform.position);
        cam = Camera.main;
    }

    void Update()
    {
        objPlane.ClosestPointOnPlane(gameObject.transform.position);
    }

    void OnMouseDown()
    {
        isDragging = true;
        mouseOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        distFromCamera = cam.WorldToScreenPoint(objPlane.ClosestPointOnPlane(gameObject.transform.position)).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = distFromCamera;

        return cam.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mouseOffset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
