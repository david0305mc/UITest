using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingSystem : MonoBehaviour
{
    [SerializeField] private LayerMask objectLayerMask;
    [SerializeField] private LayerMask landLayerMask;

    public static PickingSystem Instance { get; private set; }

    private Transform selectedObject;
    private void Awake()
    {
        Instance = this;
    }
    public bool IsPicking()
    {
        return selectedObject != null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 99f, objectLayerMask))
            {
                selectedObject = hit.transform;
                Debug.Log("hit object " + hit.ToString());
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (selectedObject != null)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 99f, landLayerMask))
                {
                    selectedObject.position = hit.point;
                    Debug.Log("hit.transform.localPosition " + hit.transform.localPosition);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
        }
        
        
    }
}
