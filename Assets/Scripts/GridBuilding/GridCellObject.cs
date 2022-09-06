using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellObject : MonoBehaviour
{
    [SerializeField] private GameObject defaultPlane;
    [SerializeField] private GameObject selectedPlane;
    
    private int x;
    private int y;

    private void Awake()
    {
        SetSelected(false);
    }

    public void SetSelected(bool val)
    {
        defaultPlane.SetActive(!val);
        selectedPlane.SetActive(val);
    }
}

