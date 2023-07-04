using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBuilding
{
    public class PlaceObjectOnGrid : MonoBehaviour
    {
        public Transform gridCellPrefab;
        public Transform cube;
        public Transform onMousePrefab;
        private Node[,] nodes;
        public Vector3 smoothMousePosition;

        [SerializeField] private int height;
        [SerializeField] private int width;
        private Plane plane;
        private Vector3 mousePosition;

        private void Start()
        {
            CreateGrid();
            plane = new Plane(Vector3.up, transform.position);
        }
        private void Update()
        {
            GetMousePositionOnGrid();
        }
        private void GetMousePositionOnGrid()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out var enter))
            {
                mousePosition = ray.GetPoint(enter);
                smoothMousePosition = mousePosition;
                mousePosition.y = 0;
                mousePosition = Vector3Int.RoundToInt(mousePosition);

                foreach (var node in nodes)
                {
                    if (node.cellPosition == mousePosition && node.isPlaceable)
                    {
                        if (Input.GetMouseButtonUp(0) && onMousePrefab != null)
                        {
                            node.isPlaceable = false;
                            onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true;
                            onMousePrefab.position = node.cellPosition + new Vector3(0, 0.5f, 0);
                            onMousePrefab = null;
                        }
                    }
                }
            }
        }

        public void OnMouseClickOnUI()
        {
            if (onMousePrefab == null)
            {
                onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);
            }
        }

        private void CreateGrid()
        {
            nodes = new Node[width, height];
            var name = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 worldPosition = new Vector3(i, 0, j);
                    Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                    obj.name = "Cell" + name;
                    nodes[i, j] = new Node(true, worldPosition, obj);
                    name++;
                }
            }
        }
    }

    public class Node
    {
        public bool isPlaceable;
        public Vector3 cellPosition;
        public Transform obj;

        public Node(bool isPlaceable, Vector3 cellPosition, Transform obj)
        {
            this.isPlaceable = isPlaceable;
            this.cellPosition = cellPosition;
            this.obj = obj;
        }
    }

}
