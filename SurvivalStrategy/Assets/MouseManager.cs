using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject hitObject = hitInfo.transform.root.gameObject;

                SelectObject(hitObject);
            }
            else
            {
                ClearSelection();
            }
        }
    }

    void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
            { 
                return;
            }
                ClearSelection();
        }

        selectedObject = obj;
        Renderer r = obj.GetComponentInChildren<Renderer>();
        Material m = r.material;
        m.color = Color.yellow;
        r.material = m;
    }

    void ClearSelection()
    {
        if (selectedObject == null)
            return;

        Renderer r = selectedObject.GetComponentInChildren<Renderer>();
        Material m = r.material;
        m.color = Color.white;
        r.material = m;
        selectedObject = null;
    }
}
