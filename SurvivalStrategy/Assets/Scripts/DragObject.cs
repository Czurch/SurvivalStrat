using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera cam;
    private float distFromCamera;
    private Vector3 mouseOffset;
    private Collider mCollider;
    private Plane objPlane;
    private Vector3 objPoint;

    void Start()
    {
        objPlane = new Plane(Vector3.up, gameObject.transform.position);
        cam = Camera.main;
        mCollider = GetComponent<Collider>();
    }

    void Update()
    {
        objPlane.ClosestPointOnPlane(gameObject.transform.position);
    }

    void OnMouseDown()
    {
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
        mCollider.enabled = false;
        transform.position = GetMouseWorldPosition() + mouseOffset;
    }

    void OnMouseUp()
    {
        mCollider.enabled = true;
        switch (gameObject.tag)
        {
            case "Survivor":
                SurvivorSnapToObject snap = gameObject.GetComponent<SurvivorSnapToObject>();
                snap.checkDistance();
                break;
        }
    }
}
