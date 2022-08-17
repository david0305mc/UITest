using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private LayerMask mouseColliderLyaerMask;
    [SerializeField] private GameObject testObject;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(testObject, GetMouseFloorPosition(), Quaternion.identity);
        }
    }

    public Vector3 GetMouseFloorPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, mouseColliderLyaerMask))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
