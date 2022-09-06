using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreviewObject : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = GridBuildingManager.Inst.GetMouseFloorPosition();

    }
}
