using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private LayerMask mouseColliderLyaerMask;
    [SerializeField] private GameObject testObject;
    private GridXZ<GridObject> grid;

    private void Start()
    {
        grid = new GridXZ<GridObject>(100, 100, 10f, new Vector3(0, 0, 0), (grid, x, y) => { return new GridObject(grid, x, y); });
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseFloorPos = GetMouseFloorPosition();
            grid.GetXZ(mouseFloorPos, out int x, out int z);
            Instantiate(testObject, grid.GetWorldPosition(x, z), Quaternion.identity);
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


public class GridObject
{
    private GridXZ<GridObject> grid;
    private int x;
    private int y;

    public GridObject(GridXZ<GridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + ", " + y + "\n";
    }

}
