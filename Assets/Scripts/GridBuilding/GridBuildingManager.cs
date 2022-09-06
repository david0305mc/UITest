using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GridBuildingManager : MonoBehaviour
{
    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private GameObject previewObjPrefab;
    [SerializeField] private LayerMask mouseColliderLyaerMask;

    private GridBuilding.GridXZ gridXZ;
    private GameObject previewObject;
    public static GridBuildingManager Inst { get; private set; }
    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        gridXZ = new GridBuilding.GridXZ(30, 30, (x, z) => {
            return Instantiate(gridCellPrefab, new Vector3(x, 0, z), Quaternion.identity, gridRoot).GetComponent<GridCellObject>();
        });

        previewObject = Instantiate(previewObjPrefab, Vector3.zero, Quaternion.identity, gridRoot);
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


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(previewObjPrefab, gridXZ.GetGridPoint(GetMouseFloorPosition()), Quaternion.identity, gridRoot);
        }

        gridXZ.SelectCell(GetMouseFloorPosition());
        //previewObject.transform.position = gridXZ.GetGridPoint(GetMouseFloorPosition());
    }
}
