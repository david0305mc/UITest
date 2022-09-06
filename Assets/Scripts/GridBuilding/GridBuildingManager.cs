using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GridBuildingManager : MonoBehaviour
{
    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private LayerMask mouseColliderLyaerMask;

    public static GridBuildingManager Inst { get; private set; }
    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        Enumerable.Range(0, 5).ToList().ForEach(x =>
        {
            Enumerable.Range(0, 5).ToList().ForEach(z =>
            {
                Instantiate(gridCellPrefab, new Vector3(x, 0, z), Quaternion.identity, gridRoot);
            });
        });
    }

    public Vector3 GetMouseFloorPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999, mouseColliderLyaerMask))
        {
            return hit.point;
        }
        else
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            plane.Raycast(ray, out float enter);
            return ray.GetPoint(enter);
        }
    }
}
