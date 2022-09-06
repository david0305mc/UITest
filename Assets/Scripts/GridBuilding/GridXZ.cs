using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridBuilding
{
    public class GridXZ : MonoBehaviour
    {
        private int width;
        private int height;

        private GridCellObject[,] gridArray;
        private GridCellObject selectedObject;

        public GridXZ(int width, int height, System.Func<int, int, GridCellObject> func)
        {
            gridArray = new GridCellObject[width, height];

            Enumerable.Range(0, width).ToList().ForEach(x =>
            {
                Enumerable.Range(0, height).ToList().ForEach(z =>
                {
                    gridArray[x,z] = func?.Invoke(x, z);
                });
            });
            this.width = width;
            this.height = height;
        }

        public Vector3Int GetGridPoint(Vector3 position)
        {
            return new Vector3Int((int)position.x, 0, (int)position.z);
        }

        public GridCellObject GetGridObject(int x, int z)
        {
            if (x >= 0 && z >= 0 && x < width && z < height)
            {
                return gridArray[x, z];
            }
            return default;
        }

        public GridCellObject GetGridObjectByPosition(Vector3 position)
        {
            return GetGridObject((int)position.x, (int)position.z);
        }

        public void SelectCell(Vector3 position)
        {
            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        gridArray[i, j].SetSelected(false);
            //    }
            //}

            if (selectedObject != default)
            {
                selectedObject.SetSelected(false);
            }
            selectedObject = GetGridObject((int)position.x, (int)position.z);
            if (selectedObject != default)
            {
                selectedObject.SetSelected(true);
            }
        }
    }

}
