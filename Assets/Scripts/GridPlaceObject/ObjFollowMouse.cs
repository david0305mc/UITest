using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GridBuilding;

public class ObjFollowMouse : MonoBehaviour
{
    private PlaceObjectOnGrid placeObjectOnGrid;
    public bool isOnGrid;

    private void Start()
    {
        placeObjectOnGrid = FindObjectOfType<PlaceObjectOnGrid>();
    }

    private void Update()
    {
        if (!isOnGrid)
        {
            transform.position = placeObjectOnGrid.smoothMousePosition + new Vector3(0, 9.5f, 0);
        }
    }

}
