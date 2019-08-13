using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private float distFromCamera;
    private Vector3 mouseOffset;
    private Collider mCollider;

    void Start()
    {
        mCollider = GetComponent<Collider>();
    }
    void OnMouseDown()
    {
        distFromCamera = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = distFromCamera;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        mCollider.enabled = false;
        transform.position = GetMouseWorldPosition() + mouseOffset;
    }

    void OnMouseUp()
    {
        mCollider.enabled = true;
    }
}
