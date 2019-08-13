using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToObject : MonoBehaviour
{
    private GameObject snappedPiece;
    private bool snapped;
    private float t;
    public float snapSpeed;

    // Start is called before the first frame update
    void Start()
    {
        snapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (snappedPiece != null)
        {
            if (snapped = true && Input.GetMouseButton(0) != true)
            {
                Debug.Log(Input.GetMouseButton(0));
                Vector3 tile_pos = snappedPiece.transform.position;
                tile_pos.y = 0.4f;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tile_pos, Time.deltaTime * snapSpeed);
            }
        }
    }

    //When we collide with a snappable Object we set our position to them
    void OnTriggerEnter(Collider collider)
    {
        //check to see if the object is snappable
        if (collider.gameObject.tag == "Snappable")
        {
            TileSpace ts = collider.gameObject.GetComponent<TileSpace>();
            //make sure the tile isnt occupied
            if (!ts.isOccupied)
            {
                //unpair from our previous tile
                if (snappedPiece != null)
                {
                    TileSpace old_tile = snappedPiece.GetComponent<TileSpace>();
                    old_tile.Unbind();
                }
                //pair the object with the tile
                Debug.Log(gameObject.name + " snapping to " + collider.gameObject.name);
                snappedPiece = collider.gameObject;
                snapped = true;
                ts.Bind(gameObject);

            }
        }
    }
    /*
    void OnTriggerExit(Collider collider)
    {
        snappedPiece = null;
        snapped = false;
    }
    */
}
