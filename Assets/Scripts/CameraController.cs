using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationAmout = 1f;
    [SerializeField] private float moveAmount = 3f;
    [SerializeField] private Vector3 zoomAmount;

    private Vector3 newPos;
    private Quaternion newRot;
    private Vector3 newZoom;
    private Vector3 dragStartPos;
    private Vector3 dragCurrPos;
    private Vector3 addPos;

    // Update is called once per frame

    private void Start()
    {
        newRot = transform.rotation;
        newZoom = new Vector3(0, 10, -10);
        cameraTransform.LookAt(transform);
    }
    void Update()
    {
        if (!PickingSystem.Instance.IsPicking())
        {
            HandleCameraByMouse();
            HandleCameraByKeyboard();
        }
    }

    private void HandleCameraByMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragStartPos = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            //addPos = Vector3.zero;
            if (Input.mousePosition.y > Screen.height - 100)
            {
                addPos += new Vector3(0, 0, 0.1f);
            }

            if (Input.mousePosition.y < 100)
            {
                addPos += new Vector3(0, 0, -0.1f);
            }

            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                dragCurrPos = ray.GetPoint(enter) - addPos;
                newPos = transform.position + dragStartPos - dragCurrPos;
                newPos = new Vector3(Mathf.Clamp(newPos.x, -5, 5), Mathf.Clamp(newPos.y, -5, 5), Mathf.Clamp(newPos.z, -5, 5));
            }
           
        }

        if (Input.GetMouseButtonUp(0))
        {
            addPos = Vector3.zero;
        }
    }

    private void HandleCameraByKeyboard()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (Input.mousePosition.y > Screen.height - 100)
        //    {
        //        newPos += new Vector3(0, 0, 1f);
        //        Debug.Log("Input.mousePosition.y" + Input.mousePosition.y);
        //    }

        //    //if (Input.mousePosition.y < 100)
        //    //{
        //    //    newPos += new Vector3(0, 0, -1f);
        //    //}
        //}
        ////Screen.width

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPos += new Vector3(0, 0, 0.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPos += new Vector3(0, 0, -0.1f);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRot *= Quaternion.Euler(Vector3.up * rotationAmout);
        }

        if (Input.GetKey(KeyCode.A))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newZoom -= zoomAmount;
        }

        newZoom = new Vector3(newZoom.x, Mathf.Clamp(newZoom.y, -10, 10), Mathf.Clamp(newZoom.z, -10, 10));
        transform.position = Vector3.Lerp(transform.position, newPos, moveAmount * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime);
    }
}

