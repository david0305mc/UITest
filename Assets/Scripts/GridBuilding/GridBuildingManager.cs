using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GridBuildingManager : MonoBehaviour
{
    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject gridCellPrefab;
    
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
}
