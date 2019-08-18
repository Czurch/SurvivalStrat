using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPosition = cam.transform.position - gameObject.transform.position;
        lookPosition.x = gameObject.transform.position.x;
        gameObject.transform.LookAt(lookPosition, Vector3.up);
    }
}
