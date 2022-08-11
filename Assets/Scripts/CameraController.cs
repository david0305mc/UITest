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
    // Update is called once per frame

    private void Start()
    {
        newRot = transform.rotation;
        newZoom = cameraTransform.localPosition;

    }
    void Update()
    {
        HandleCameraByMouse();
        HandleCameraByKeyboard();
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
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                dragCurrPos = ray.GetPoint(enter);
                newPos = transform.position + dragStartPos - dragCurrPos;
            }
        }
    }

    private void HandleCameraByKeyboard()
    {
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

        newZoom = new Vector3(Mathf.Clamp(newZoom.x, 0, 10), Mathf.Clamp(newZoom.y, 0, 10), Mathf.Clamp(newZoom.z, 0, 10));
        transform.position = Vector3.Lerp(transform.position, newPos, moveAmount * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime);
    }
}

