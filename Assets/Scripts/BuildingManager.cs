using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [SerializeField] private LayerMask mouseColliderLyaerMask;
    [SerializeField] private GameObject testObject;
    [SerializeField] private GameObject cellObject;
    [SerializeField] private PlacedObjectTypeSO placedObject;

    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    private GridXZ<GridObject> grid;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int x = 0; x < 30; x++)
        {
            for (int y = 0; y < 30; y++)
            {
                Instantiate(cellObject, new Vector3(x * 10, 0, y * 10), Quaternion.identity);
            }
        }
        grid = new GridXZ<GridObject>(30, 30, 10f, new Vector3(0, 0, 0), (grid, x, y) => { return new GridObject(grid, x, y); });
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseFloorPos = GetMouseFloorPosition();
            grid.GetXZ(mouseFloorPos, out int x, out int z);

            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList(new Vector2Int(x, z), dir);

            bool canBuild = true;

            foreach (var gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                Vector2Int rotationOffset = placedObject.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                var builtTransform = 
                    Instantiate(
                        placedObject.prefab,
                        placedObjectWorldPosition,
                        Quaternion.Euler(0, placedObject.GetRotationAngle(dir), 0)
                        );
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                }
            }
            else
            {
                UtilsClass.CreateWorldTextPopup("Cannot build hero!", mouseFloorPos);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            var mouseFloorPos = GetMouseFloorPosition();
            UtilsClass.CreateWorldTextPopup("" + dir, mouseFloorPos);
        }
    }
    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseFloorPosition();
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
    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = GetMouseFloorPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObject != null)
        {
            Vector2Int rotationOffset = placedObject.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }
    }
    public Quaternion GetPlacedObjectRotation()
    {
        if (placedObject != null)
        {
            return Quaternion.Euler(0, placedObject.GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO()
    {
        return placedObject;
    }
}


public class GridObject
{
    private GridXZ<GridObject> grid;
    private int x;
    private int z;
    private Transform transform;

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void ClearTransform()
    {
        this.transform = null;
        grid.TriggerGridObjectChanged(x, z);
    }

    public bool CanBuild()
    {
        return transform == null;
    }

    public GridObject(GridXZ<GridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.z = y;
    }

    public override string ToString()
    {
        return x + ", " + z + "\n" + transform;
    }

}
