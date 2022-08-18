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
                else
                {
                    Plane plane = new Plane(Vector3.up, Vector3.zero);
                    if (plane.Raycast(ray, out float enter))
                    {
                        var point = ray.GetPoint(enter);
                        selectedObject.position = point;
                        Debug.Log("hit.transform.localPosition " + point);
                    }
                }
                //var mainCamera = Camera.main;
                ////addPos = Vector3.zero;
                //if (Input.mousePosition.y > Screen.height - 100)
                //{
                //    mainCamera.transform.position += mainCamera.transform.position + new Vector3(0, 0, 0.5f);
                //}

                //if (Input.mousePosition.y < 100)
                //{
                //    mainCamera.transform.position += mainCamera.transform.position + new Vector3(0, 0, -0.5f);
                //}

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
        }
        
        
    }
}
